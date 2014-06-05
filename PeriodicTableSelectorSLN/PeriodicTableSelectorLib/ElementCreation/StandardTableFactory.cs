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
			m_elements			= elements.ToArray();

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
			// I know this isn't the most flexable way to do this. A database,
			// ini, json, or xml file would be a much better place to store
			// information. Using one of those solutions would also make it
			// easy to fix a typo for someone that doesn't have access to the
			// source code. Then again, this is the Periodic Table, which
			// changes how often? And, people do have access to the source
			// code. I'm not saying that I won't change it to one of the above
			// solutions, but for now, this works just fine.
			// -FCT
			List<ChemicalElement> elements = new List<ChemicalElement>(118);

			///
			/// Elements 1 - 5
			/// 
			CreateNewElement(1, "H",  "Hydrogen",	 1.008,		elements);
			CreateNewElement(2, "He", "Helium",		 4.002602,	elements);
			CreateNewElement(3, "Li", "Lithium",	 4.94,		elements);
			CreateNewElement(4, "Be", "Beryllium",   9.012182,	elements);
			CreateNewElement(5, "B",  "Boron",		10.81,		elements);

			///
			/// Elements 6 - 10
			/// 
			CreateNewElement(6,  "C",  "Carbon",	12.011,		elements);
			CreateNewElement(7,  "N",  "Nitrogen",	14.007,		elements);
			CreateNewElement(8,  "O",  "Oxygen",	15.999,		elements);
			CreateNewElement(9,  "F",  "Fluorine",	18.9984032, elements);
			CreateNewElement(10, "Ne", "Neon",		20.1797,	elements);

			///
			/// Elements 11 - 15
			/// 
			CreateNewElement(11, "Na", "Sodium",		22.98976928,	elements);
			CreateNewElement(12, "Mg", "Magnesium",		24.305,			elements);
			CreateNewElement(13, "Al", "Aluminium",		26.9815386,		elements);
			CreateNewElement(14, "Si", "Silicon",		28.085,			elements);
			CreateNewElement(15, "P",  "Phosphorus",	30.973762,		elements);

			///
			/// Elements 16 - 20
			/// 
			CreateNewElement(16, "S",  "Sulfur",	32.06,		elements);
			CreateNewElement(17, "Cl", "Chlorine",	35.45,		elements);
			CreateNewElement(18, "Ar", "Argon",		39.948,		elements);
			CreateNewElement(19, "K",  "Potassium", 39.0983,	elements);
			CreateNewElement(20, "Ca", "Calcium",	40.078,		elements);

			///
			/// Elements 21 - 25
			/// 
			CreateNewElement(21, "Sc", "Scandium",	44.955912,	elements);
			CreateNewElement(22, "Ti", "Titanium",	47.867,		elements);
			CreateNewElement(23, "V",  "Vanadium",	50.9415,	elements);
			CreateNewElement(24, "Cr", "Chromium",	51.9961,	elements);
			CreateNewElement(25, "Mn", "Manganese",	54.938045,	elements);

			///
			/// Elements 26 - 30
			/// 
			CreateNewElement(26, "Fe", "Iron",		55.845,		elements);
			CreateNewElement(27, "Co", "Cobalt",	58.933195,	elements);
			CreateNewElement(28, "Ni", "Nickel",	58.6934,	elements);
			CreateNewElement(29, "Cu", "Copper",	63.546,		elements);
			CreateNewElement(30, "Zn", "Zinc",		65.38,		elements);

			///
			/// Elements 31 - 35
			/// 
			CreateNewElement(31, "Ga", "Gallium",	69.723,		elements);
			CreateNewElement(32, "Ge", "Germinium", 72.630,		elements);
			CreateNewElement(33, "As", "Arsenic",	74.92160,	elements);
			CreateNewElement(34, "Se", "Selenium",	78.96,		elements);
			CreateNewElement(35, "Br", "Bromine",	79.904,		elements);

			///
			/// Elements 36 - 40
			/// 
			CreateNewElement(36, "Kr", "Krypton",	83.798,		elements);
			CreateNewElement(37, "Rb", "Rubidium",	85.4678,	elements);
			CreateNewElement(38, "Sr", "Strontium", 87.62,		elements);
			CreateNewElement(39, "Y",  "Yttrium",	88.90585,	elements);
			CreateNewElement(40, "Zr", "Zirconium", 91.224,		elements);

			///
			/// Elements 41 - 45
			/// 
			CreateNewElement(41, "Nb", "Niobium",		 92.90638,	elements);
			CreateNewElement(42, "Mo", "Molybdenum",	 95.96,		elements);
			CreateNewElement(43, "Tc", "Technetium",	 98,		elements);
			CreateNewElement(44, "Ru", "Ruthenium",		101.07,		elements);
			CreateNewElement(45, "Rh", "Rhodium",		102.90550,	elements);

			///
			/// Elements 46 - 50
			/// 
			CreateNewElement(46, "Pd", "Palladium", 106.42,		elements);
			CreateNewElement(47, "Ag", "Silver",	107.8682,	elements);
			CreateNewElement(48, "Cd", "Cadmium",	112.411,	elements);
			CreateNewElement(49, "In", "Indium",	114.818,	elements);
			CreateNewElement(50, "Sn", "Tin",		118.710,	elements);

			///
			/// Elements 51 - 55
			/// 
			CreateNewElement(51, "Sb", "Antimony", 121.760, elements);
			CreateNewElement(52, "Te", "Tellurium", 127.60, elements);
			CreateNewElement(53, "I", "Iodine", 126.90447, elements);
			CreateNewElement(54, "Xe", "Xenon", 131.293, elements);
			CreateNewElement(55, "Cs", "Caesium", 132.9054519, elements);

			///
			/// Elements 56 - 60
			/// 
			CreateNewElement(56, "Ba", "Barium", 137.327, elements);
			CreateNewElement(57, "La", "Lanthanum", 138.90547, elements);
			CreateNewElement(58, "Ce", "Cerium", 140.116, elements);
			CreateNewElement(59, "Pr", "Praseodymium", 140.90765, elements);
			CreateNewElement(60, "Nd", "Neodymium", 144.242, elements);

			return elements;
		}

		///<summary>
		/// I'm doing this on a laptop, there is only so much screen space 
		/// to work with. This method makes it a lot easier to read the list 
		/// of chemical elements as I type the information in.
		/// -FCT
		/// </summary>
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
