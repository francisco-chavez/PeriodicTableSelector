using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Unv.PeriodicTableSelectorLib.ElementCreation
{
	public class StandardTableFactory
		: FactoryBase
	{
		#region Attributes
		private Dictionary<int, ChemicalElement>	m_atomicNumberIndex;
		private Dictionary<string, ChemicalElement> m_nameIndex;

		private				ChemicalElement[]		m_elements;
		private readonly	string[]				m_elementNames;
		private readonly	string[]				m_elementSymbols;
		#endregion


		#region Properties
		public override ChemicalElement[] Elements
		{
			get
			{
				ChemicalElement[] copy = new ChemicalElement[m_elements.Length];
				m_elements.CopyTo(copy, 0);
				return copy;
			}
		}

		public override string[] ElementNames
		{
			get { return m_elementNames; }
		}

		public override string[] ElementSymbols
		{
			get { return m_elementSymbols; }
		}
		#endregion


		#region Constructors
		public StandardTableFactory()
		{
			m_atomicNumberIndex = new Dictionary<int, ChemicalElement>(118);
			m_nameIndex			= new Dictionary<string, ChemicalElement>(236);

			var elements = CreateElements();

			m_elementNames		= elements.Select(chem => { return chem.ChemicalName; }).ToArray();
			m_elementSymbols	= elements.Select(chem => { return chem.Symbol; }).ToArray();

			foreach (var element in elements)
			{
				m_atomicNumberIndex.Add(element.AtomicNumber, element);
				m_nameIndex.Add(element.ChemicalName.ToLower(), element);
				m_nameIndex.Add(element.Symbol.ToLower(), element);
			}
		}
		#endregion


		#region Methods
		public override bool HasElement(int atomicNumber)
		{
			return m_atomicNumberIndex.ContainsKey(atomicNumber);
		}

		public override bool HasElement(string nameOrSymbol)
		{
			return m_nameIndex.ContainsKey(nameOrSymbol.ToLower());
		}

		public override ChemicalElement Element(int atomicNumber)
		{
			return m_atomicNumberIndex[atomicNumber];
		}

		public override ChemicalElement Element(string nameOrSymbol)
		{
			return m_nameIndex[nameOrSymbol.ToLower()];
		}

		private List<ChemicalElement> CreateElements()
		{
			List<ChemicalElement> elements = new List<ChemicalElement>(118);

			///
			/// Elements 1 - 5
			/// 
			CreateNewElement(1, "H",  "Hydrogen",	 1.008,		elements);
			CreateNewElement(2, "He", "Helium",		 4.002602,	elements);
			CreateNewElement(3, "Li", "Lithium",	 4.94,		elements);
			CreateNewElement(4, "Be", "Beryllium",   9.012182,	elements);
			CreateNewElement(5, "B",  "Boron",		10.81,		elements);


			return elements;
		}

		private void CreateNewElement(int atomicNumer, string symbol, string name, double meanAtomicMass, List<ChemicalElement> elements)
		{
			var element = 
				new ChemicalElement() 
				{ 
					AtomicNumber	= atomicNumer, 
					Symbol			= symbol, 
					ChemicalName	= name, 
					AtomicMass		= meanAtomicMass 
				};

			elements.Add(element);
		}
		#endregion
	}
}
