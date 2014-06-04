using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Unv.PeriodicTableSelectorLib.ElementCreation
{
	public abstract class FactoryBase
	{
		public abstract ChemicalElement[]	Elements		{ get; }
		public abstract string[]			ElementNames	{ get; }
		public abstract string[]			ElementSymbols	{ get; }


		public abstract bool			HasElement(int atomicNumber);
		public abstract bool			HasElement(string nameOrSymbol);
		public abstract ChemicalElement Element(int atomicNumber);
		public abstract ChemicalElement Element(string nameOrSymbol);
	}
}
