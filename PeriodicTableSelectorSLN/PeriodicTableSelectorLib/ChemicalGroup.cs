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
		: IEnumerable<ChemicalElement>
	{
		#region Attributes
		private List<ChemicalElement> m_elements;
		#endregion


		#region Properties
		public FactoryBase	Factory			{ get; private set; }
		public string		GroupName		{ get; set; }
		public bool			CanBeUnlocked	{ get; private set; }
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
		#endregion


		#region Methods
		public static ChemicalGroup Union(ChemicalGroup a, ChemicalGroup b) { throw new NotImplementedException(); }
		public static ChemicalGroup Inersection(ChemicalGroup a, ChemicalGroup b) { throw new NotImplementedException(); }
		public static ChemicalGroup Complement(ChemicalGroup a, ChemicalGroup b) { throw new NotImplementedException(); }

		public void AddChemicalElement(int atomicNumber)
		{
			// If we already have it, then don't add it
			if (m_elements.Any(chem => { return chem.AtomicNumber == atomicNumber; }))
				return;

			m_elements.Add(Factory.Element(atomicNumber));
		}

		public void AddChemicalElement(string symbolOrName)
		{
			if(string.IsNullOrWhiteSpace(symbolOrName))
				throw new ArgumentNullException();

			// If we already have it, then don't put it in
			if (m_elements.Any(chem => { return symbolOrName.Equals(chem.Symbol, StringComparison.OrdinalIgnoreCase) || symbolOrName.Equals(chem.ChemicalName, StringComparison.OrdinalIgnoreCase); }))
				return;

			m_elements.Add(Factory.Element(symbolOrName));
		}

		public void RemoveChemicalElement(int atomicNumber)
		{
			var chemicals = (from ChemicalElement chem in m_elements
							 where chem.AtomicNumber == atomicNumber
							 select chem).ToArray();

			foreach (var chem in chemicals)
				m_elements.Remove(chem);
		}

		public void RemoveChemicalElement(string symbolOrName)
		{
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
			foreach (var chem in m_elements)
				chem.IsChecked = true;
		}

		public void DeselectChemicals()
		{
			foreach (var chem in m_elements)
				chem.IsChecked = false;
		}

		public void InvertChemicalSelections()
		{
			foreach (var chem in m_elements)
				chem.IsChecked = !chem.IsChecked;
		}


		public void SetGlowBrush(Brush glowBrush)
		{
			foreach (var chem in m_elements)
				chem.GlowBrush = glowBrush;
		}

		public void SetBackground(Brush backgroundBrush)
		{
			foreach (var chem in m_elements)
				chem.Background = backgroundBrush;
		}

		public void SetForeground(Brush foregroundBrush)
		{
			foreach (var chem in m_elements)
				chem.Foreground = foregroundBrush;
		}


		public void LockGroupMembers()
		{
			IsLocked = true;
		}

		public bool UnlockGroupMembers()
		{
			if (!CanBeUnlocked)
				return false;

			IsLocked = false;
			return true;
		}


		public IEnumerator<ChemicalElement> GetEnumerator()
		{
			return m_elements.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) m_elements).GetEnumerator();
		}
		#endregion
	}
}
