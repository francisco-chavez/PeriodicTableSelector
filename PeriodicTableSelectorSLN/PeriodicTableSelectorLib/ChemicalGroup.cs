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

		public void AddChemicalElement(int atomicNumber) { }
		public void AddChemicalElement(string symbolOrName) { }
		public void RemoveChemicalElement(int atomicNumber) { }
		public void RemoveChemicalElement(string symbolOrName) { }

		public void SelectChemicals() { }
		public void DeselectChemicals() { }
		public void InvertChemicalSelections() { }

		public void SetGlowBrush(Brush glowBrush) { }
		public void SetBackground(Brush backgroundBrush) { }
		public void SetForeground(Brush foregroundBrush) { }

		public void LockGroupMembers() { }
		public bool UnlockGroupMembers() { return CanBeUnlocked; }


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
