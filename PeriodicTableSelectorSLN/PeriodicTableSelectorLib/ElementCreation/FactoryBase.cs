using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


namespace Unv.PeriodicTableSelectorLib.ElementCreation
{
	public abstract class FactoryBase
	{
		#region Properties
		public abstract ChemicalElement[]						Elements			{ get; }
		public abstract string[]								ElementNames		{ get; }
		public abstract string[]								ElementSymbols		{ get; }
		public abstract List<ChemicalGroup>						ChemicalGroups		{ get; }
		public abstract ObservableCollection<ChemicalElement>	SelectedElements	{ get; protected set; }
		#endregion


		#region Methods
		public abstract bool				HasElement(int atomicNumber);
		public abstract bool				HasElement(string nameOrSymbol);
		public abstract ChemicalElement		Element(int atomicNumber);
		public abstract ChemicalElement		Element(string nameOrSymbol);
		#endregion
	}
}
