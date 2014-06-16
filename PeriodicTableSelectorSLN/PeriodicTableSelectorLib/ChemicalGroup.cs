using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

using Unv.PeriodicTableSelectorLib.ElementCreation;


namespace Unv.PeriodicTableSelectorLib
{
	public class ChemicalGroup
		: IEnumerable<ChemicalElement>, IDisposable
	{
		#region Attributes
		private List<ChemicalElement>	m_elements;
		private bool					isDisposed = false;
		#endregion


		#region Properties
		/// <summary>
		/// Gets the Factory owning the chemicals in this group.
		/// </summary>
		public FactoryBase	Factory			{ get; private set; }

		public string		GroupName		{ get; set; }

		/// <summary>
		/// Gets a bool inidcating if the groups membership can
		/// be unlocked once it has been locked.
		/// </summary>
		public bool			CanBeUnlocked	{ get; private set; }

		/// <summary>
		/// Gets a bool indicating if the membership to this group
		/// is currently locked. Chemical Elements cannot be added
		/// to or removed from the group while locked.
		/// </summary>
		public bool			IsLocked		{ get; private set; }
		#endregion


		#region Constructors
		public ChemicalGroup(FactoryBase chemicalFactory, bool canBeUnlocked = true)
		{
			if (chemicalFactory == null)
				throw new ArgumentNullException();

			IsLocked		= false;
			CanBeUnlocked	= canBeUnlocked;
			m_elements		= new List<ChemicalElement>();
			Factory			= chemicalFactory;
		}

		~ChemicalGroup()
		{
			this.Dispose();
		}
		#endregion


		#region Methods
		/// <summary>
		/// Returns a Chemical Group containing the set union of 'a' and  'b'.
		/// </summary>
		public static ChemicalGroup Union(ChemicalGroup a, ChemicalGroup b)
		{
			SetCheck(a, b);

			var atomiNumbers = new HashSet<int>(a.Select(chem => { return chem.AtomicNumber; }));
			atomiNumbers.UnionWith(b.Select(chem => { return chem.AtomicNumber; }));

			ChemicalGroup result = new ChemicalGroup(a.Factory);

			foreach (var atomicNumber in atomiNumbers.ToArray())
				result.AddChemicalElement(atomicNumber);

			return result;
		}

		/// <summary>
		/// Returns a Chemical Group containing the set intersection of 'a' and 'b'.
		/// </summary>
		public static ChemicalGroup Inersection(ChemicalGroup a, ChemicalGroup b)
		{
			SetCheck(a, b);

			var atomicNumbers = new HashSet<int>(a.Select(chem => { return chem.AtomicNumber; }));
			atomicNumbers.IntersectWith(b.Select(chem => { return chem.AtomicNumber; }));

			ChemicalGroup result = new ChemicalGroup(a.Factory);
			foreach (var atomicNumber in atomicNumbers.ToArray())
				result.AddChemicalElement(atomicNumber);

			return result;
		}
		
		/// <summary>
		/// Returns a Chemical Group contains the relative complement 
		/// of 'b' in 'a'. In other words 'a' minus 'b'.
		/// </summary>
		public static ChemicalGroup Complement(ChemicalGroup a, ChemicalGroup b)
		{
			SetCheck(a, b);

			HashSet<int> atomicNumbers = new HashSet<int>(a.Select(chem => { return chem.AtomicNumber; }));
			atomicNumbers.ExceptWith(b.Select(chem => { return chem.AtomicNumber; }));

			ChemicalGroup result = new ChemicalGroup(a.Factory);

			foreach (var atomicNumber in atomicNumbers.ToArray())
				result.AddChemicalElement(atomicNumber);

			return result;
		}

		/// <summary>
		/// Checks to see if both Chemical Groups that are passed in are valid for a
		/// set operation. If they are not, then an exception will be thrown.
		/// </summary>
		private static void SetCheck(ChemicalGroup a, ChemicalGroup b)
		{
			if (a == null || b == null)
				throw new ArgumentNullException("Both groups must be filled in.");
			if (a.Factory != b.Factory)
				throw new ArgumentException("Chemical Groups A and B come from different factories.");
			if (a.isDisposed || b.isDisposed)
				throw new ArgumentException("Cannot perform set operations on disposed Chemical Groups.");
		}


		public void AddChemicalElement(int atomicNumber)
		{
			DisposeCheck();

			if (IsLocked)
				return;

			// If we already have it, then don't add it
			if (m_elements.Any(chem => { return chem.AtomicNumber == atomicNumber; }))
				return;

			m_elements.Add(Factory.Element(atomicNumber));
		}

		public void AddChemicalElement(string symbolOrName)
		{
			DisposeCheck();

			if (IsLocked)
				return;

			if(string.IsNullOrWhiteSpace(symbolOrName))
				throw new ArgumentNullException();

			// If we already have it, then don't put it in
			if (m_elements.Any(chem => { return symbolOrName.Equals(chem.Symbol, StringComparison.OrdinalIgnoreCase) || symbolOrName.Equals(chem.ChemicalName, StringComparison.OrdinalIgnoreCase); }))
				return;

			m_elements.Add(Factory.Element(symbolOrName));
		}

		public void AddChemicalElements(ChemicalGroup group)
		{
			if (this.IsLocked)
				return;

			if (group == null)
				throw new ArgumentNullException();

			HashSet<int> currentKeys = new HashSet<int>(m_elements.Select(chem => { return chem.AtomicNumber; }));
			HashSet<int> newKeys = new HashSet<int>(group.m_elements.Select(chem => { return chem.AtomicNumber; }));

			newKeys.ExceptWith(currentKeys.ToArray());
			foreach (var key in newKeys)
				AddChemicalElement(key);
		}

		public void RemoveChemicalElement(int atomicNumber)
		{
			DisposeCheck();

			if (IsLocked)
				return;

			var chemicals = (from ChemicalElement chem in m_elements
							 where chem.AtomicNumber == atomicNumber
							 select chem).ToArray();

			foreach (var chem in chemicals)
				m_elements.Remove(chem);
		}

		public void RemoveChemicalElement(string symbolOrName)
		{
			DisposeCheck();

			if (IsLocked)
				return;

			if(string.IsNullOrWhiteSpace(symbolOrName))
				throw new ArgumentNullException();

			var chemicals = (from ChemicalElement chem in m_elements
							 where symbolOrName.Equals(chem.ChemicalName, StringComparison.OrdinalIgnoreCase) ||
								   symbolOrName.Equals(chem.Symbol, StringComparison.OrdinalIgnoreCase)
							 select chem).ToArray();

			foreach (var chem in chemicals)
				m_elements.Remove(chem);
		}


		public void SelectChemicals()
		{
			DisposeCheck();

			foreach (var chem in m_elements)
				chem.IsChecked = true;
		}

		public void DeselectChemicals()
		{
			DisposeCheck();

			foreach (var chem in m_elements)
				chem.IsChecked = false;
		}

		public void InvertChemicalSelections()
		{
			DisposeCheck();

			foreach (var chem in m_elements)
				chem.IsChecked = !chem.IsChecked;
		}


		public void SetGlowBrush(Brush glowBrush)
		{
			DisposeCheck();

			foreach (var chem in m_elements)
				chem.GlowBrush = glowBrush;
		}

		public void SetBackground(Brush backgroundBrush)
		{
			DisposeCheck();

			foreach (var chem in m_elements)
				chem.Background = backgroundBrush;
		}

		public void SetForeground(Brush foregroundBrush)
		{
			DisposeCheck();

			foreach (var chem in m_elements)
				chem.Foreground = foregroundBrush;
		}


		public void LockGroupMembers()
		{
			DisposeCheck();

			IsLocked = true;
		}

		public bool UnlockGroupMembers()
		{
			DisposeCheck();

			if (!CanBeUnlocked)
				return false;

			IsLocked = false;
			return true;
		}


		public IEnumerator<ChemicalElement> GetEnumerator()
		{
			DisposeCheck();

			return m_elements.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			DisposeCheck();

			return ((IEnumerable) m_elements).GetEnumerator();
		}


		public void Dispose()
		{
			if (isDisposed)
				return;

			isDisposed = true;
			m_elements.Clear();
			m_elements = null;
		}

		private void DisposeCheck()
		{
			if (isDisposed)
				throw new InvalidOperationException("This Chemical Group is currently Dispoased");
		}
		#endregion
	}
}
