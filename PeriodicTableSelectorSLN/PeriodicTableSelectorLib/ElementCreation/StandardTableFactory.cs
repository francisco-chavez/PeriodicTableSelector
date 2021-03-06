﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;


namespace Unv.PeriodicTableSelectorLib.ElementCreation
{
	public class StandardTableFactory
		: FactoryBase
	{
		#region Attributes
		private Dictionary<int, ChemicalElement>	m_atomicNumberIndex;
		private Dictionary<string, ChemicalElement> m_nameIndex;

		private				ChemicalElement[]		m_elements;
		private				List<ChemicalGroup>		m_chemicalGroups;
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

		public override List<ChemicalGroup> ChemicalGroups
		{
			get { return m_chemicalGroups; }
		}

		public override ObservableCollection<ChemicalElement> SelectedElements
		{
			get { return m_selectedElements; }
			protected set
			{
				if (m_selectedElements == value)
					return;

				if (m_selectedElements != null)
					m_selectedElements.CollectionChanged -= SelectedElements_CollectionChanged;

				m_selectedElements = value;

				if (m_selectedElements != null)
					m_selectedElements.CollectionChanged += SelectedElements_CollectionChanged;
			}
		}
		private ObservableCollection<ChemicalElement> m_selectedElements;
		#endregion


		#region Constructors
		public StandardTableFactory()
		{
			SelectedElements = new ObservableCollection<ChemicalElement>();

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

			m_chemicalGroups = new List<ChemicalGroup>();
			BuildStandardGroups();
		}
		#endregion


		#region Event Handlers
		void SelectedElements_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
			case NotifyCollectionChangedAction.Add:
				if(e.NewItems != null)
					foreach (var item in e.NewItems)
					{
						if (!(item is ChemicalElement))
							throw new Exception("Non-Chemical-Element added to selected items list.");

						var chem = item as ChemicalElement;
						if (!m_elements.Contains(chem))
							throw new Exception("A Chemical Element that doesn't belong to this factory as been added to the selected items list.");

						chem.IsChecked = true;
					}
				break;

			case NotifyCollectionChangedAction.Remove:
				if(e.OldItems != null)
					foreach (var item in e.OldItems)
					{
						var chem = item as ChemicalElement;
						if (chem == null)
							continue;

						chem.IsChecked = false;
					}
				break;

			default:
				break;
			}
		}

		void Element_Unchecked(object sender, RoutedEventArgs e)
		{
			var source = sender as ChemicalElement;
			if (source != null)
				if (SelectedElements.Contains(source))
					SelectedElements.Remove(source);
		}

		void Element_Checked(object sender, RoutedEventArgs e)
		{
			var source = sender as ChemicalElement;
			if (source != null)
				if (!SelectedElements.Contains(source))
					SelectedElements.Add(source);
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


		protected virtual void BuildStandardGroups()
		{
			ChemicalGroup group = null;

			// All Elements
			group = new ChemicalGroup(this, false);
			group.GroupName = "All Chemical Elements";
			foreach (var chem in this.Elements)
				group.AddChemicalElement(chem.AtomicNumber);
			this.ChemicalGroups.Add(group);

			// Alkali Metals
			group = new ChemicalGroup(this, false);
			group.GroupName = "Alkali Metals";
			this.ChemicalGroups.Add(group);

			group.AddChemicalElement("Lithium");
			group.AddChemicalElement("Sodium");
			group.AddChemicalElement("Potassium");
			group.AddChemicalElement("Rubidium");
			group.AddChemicalElement("Caesium");
			group.AddChemicalElement("Francium");

			group.LockGroupMembers();


			// Alkaline Earth Metals
			group = new ChemicalGroup(this, false);
			group.GroupName = "Alkaline Earth Metals";
			this.ChemicalGroups.Add(group);

			group.AddChemicalElement("Beryllium");
			group.AddChemicalElement("Magnesium");
			group.AddChemicalElement("Calcium");
			group.AddChemicalElement("Strontium");
			group.AddChemicalElement("Barium");
			group.AddChemicalElement("Radium");

			group.LockGroupMembers();


			// Transition Metals
			group = new ChemicalGroup(this, false);
			group.GroupName = "Transition Metals";
			this.ChemicalGroups.Add(group);

			group.AddChemicalElement("Scandium");
			group.AddChemicalElement("Titanium");
			group.AddChemicalElement("Vanadium");
			group.AddChemicalElement("Chromium");
			group.AddChemicalElement("Manganese");

			group.AddChemicalElement("Iron");
			group.AddChemicalElement("Cobalt");
			group.AddChemicalElement("Nickel");
			group.AddChemicalElement("Copper");
			group.AddChemicalElement("Zinc");

			group.AddChemicalElement("Yttrium");
			group.AddChemicalElement("Zirconium");
			group.AddChemicalElement("Niobium");
			group.AddChemicalElement("Molybdenum");
			group.AddChemicalElement("Technetium");

			group.AddChemicalElement("Ruthenium");
			group.AddChemicalElement("Rhodium");
			group.AddChemicalElement("Palladium");
			group.AddChemicalElement("Silver");
			group.AddChemicalElement("Cadmium");

			group.AddChemicalElement("Hafnium");
			group.AddChemicalElement("Tantalum");
			group.AddChemicalElement("Tungsten");
			group.AddChemicalElement("Rhenium");
			group.AddChemicalElement("Osmium");

			group.AddChemicalElement("Iridium");
			group.AddChemicalElement("Platinum");
			group.AddChemicalElement("Gold");
			group.AddChemicalElement("Mercury");
			group.AddChemicalElement("Rutherfordium");

			group.AddChemicalElement("Dubnium");
			group.AddChemicalElement("Seaborgium");
			group.AddChemicalElement("Bohrium");
			group.AddChemicalElement("Hassium");
			group.AddChemicalElement("Meitnerium");

			group.AddChemicalElement("Darmstadtium");
			group.AddChemicalElement("Roetgenium");
			group.AddChemicalElement("Copernicium");

			group.LockGroupMembers();


			// Other Metals
			group = new ChemicalGroup(this, false);
			group.GroupName = "Other Metals";
			this.ChemicalGroups.Add(group);

			group.AddChemicalElement("Aluminium");
			group.AddChemicalElement("Gallium");
			group.AddChemicalElement("Indium");
			group.AddChemicalElement("Tin");
			group.AddChemicalElement("Thallium");
			group.AddChemicalElement("Lead");
			group.AddChemicalElement("Bismuth");
			group.AddChemicalElement("Ununtrium");
			group.AddChemicalElement("Flerovium");
			group.AddChemicalElement("Ununpentium");
			group.AddChemicalElement("Livermorium");

			group.LockGroupMembers();


			// Metalloids
			group = new ChemicalGroup(this, false);
			group.GroupName = "Metalloids";
			this.ChemicalGroups.Add(group);

			group.AddChemicalElement("Boron");
			group.AddChemicalElement("Silicon");
			group.AddChemicalElement("Germanium");
			group.AddChemicalElement("Arsenic");
			group.AddChemicalElement("Antimony");
			group.AddChemicalElement("Tellurium");
			group.AddChemicalElement("Polonium");
			group.AddChemicalElement("Ununseptium");

			group.LockGroupMembers();


			// Non-Metals
			group = new ChemicalGroup(this, false);
			group.GroupName = "Non-Metals";
			this.ChemicalGroups.Add(group);

			group.AddChemicalElement("Hydrogen");
			group.AddChemicalElement("Carbon");
			group.AddChemicalElement("Nitrogen");
			group.AddChemicalElement("Oxygen");
			group.AddChemicalElement("Phosphorus");
			group.AddChemicalElement("Sulfur");
			group.AddChemicalElement("Selenium");

			group.LockGroupMembers();


			// Halogens
			group = new ChemicalGroup(this, false);
			group.GroupName = "Halogens";
			this.ChemicalGroups.Add(group);

			group.AddChemicalElement("Fluorine");
			group.AddChemicalElement("Chlorine");
			group.AddChemicalElement("Bromine");
			group.AddChemicalElement("Iodine");
			group.AddChemicalElement("Astatine");

			group.LockGroupMembers();

			
			// Noble Gases
			group = new ChemicalGroup(this, false);
			group.GroupName = "Noble Gases";
			this.ChemicalGroups.Add(group);

			group.AddChemicalElement("Helium");
			group.AddChemicalElement("Neon");
			group.AddChemicalElement("Argon");
			group.AddChemicalElement("Krypton");
			group.AddChemicalElement("Xenon");
			group.AddChemicalElement("Radon");
			group.AddChemicalElement("Ununoctium");

			group.LockGroupMembers();


			///
			/// Rare Earth Elements
			/// 
			
			// Lanthanum Series
			var lanthanide = new ChemicalGroup(this, false);
			group = lanthanide;
			group.GroupName = "Lanthanide Series";
			this.ChemicalGroups.Add(group);

			group.AddChemicalElement("Lanthanum");
			group.AddChemicalElement("Cerium");
			group.AddChemicalElement("Praseodymium");
			group.AddChemicalElement("Neodymium");
			group.AddChemicalElement("Promethium");

			group.AddChemicalElement("Samarium");
			group.AddChemicalElement("Europium");
			group.AddChemicalElement("Gadolinium");
			group.AddChemicalElement("Terbium");
			group.AddChemicalElement("Dysprosium");

			group.AddChemicalElement("Holmium");
			group.AddChemicalElement("Erbium");
			group.AddChemicalElement("Thulium");
			group.AddChemicalElement("Ytterbium");
			group.AddChemicalElement("Lutetium");

			group.LockGroupMembers();


			// Actinide Series
			var actinium = new ChemicalGroup(this, false);
			group = actinium;
			group.GroupName = "Actinide Series";
			this.ChemicalGroups.Add(group);

			group.AddChemicalElement("Actinium");
			group.AddChemicalElement("Thorium");
			group.AddChemicalElement("Protactinium");
			group.AddChemicalElement("Uranium");
			group.AddChemicalElement("Neptunium");

			group.AddChemicalElement("Plutonium");
			group.AddChemicalElement("Americium");
			group.AddChemicalElement("Curium");
			group.AddChemicalElement("Berkelium");
			group.AddChemicalElement("Californium");

			group.AddChemicalElement("Einsteinium");
			group.AddChemicalElement("Fermium");
			group.AddChemicalElement("Mendelevium");
			group.AddChemicalElement("Nobelium");
			group.AddChemicalElement("Lawrencium");

			group.LockGroupMembers();


			// Rare Earth Elements
			group = new ChemicalGroup(this, false);
			group.GroupName = "Rare Earth Elements";
			this.ChemicalGroups.Add(group);

			group.AddChemicalElements(lanthanide);
			group.AddChemicalElements(actinium);

			group.LockGroupMembers();
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
			/// Elements 1 - 10
			/// 
			CreateNewElement(1, "H",  "Hydrogen",	 1.008,		elements);
			CreateNewElement(2, "He", "Helium",		 4.002602,	elements);
			CreateNewElement(3, "Li", "Lithium",	 4.94,		elements);
			CreateNewElement(4, "Be", "Beryllium",   9.012182,	elements);
			CreateNewElement(5, "B",  "Boron",		10.81,		elements);

			CreateNewElement(6,  "C",  "Carbon",	12.011,		elements);
			CreateNewElement(7,  "N",  "Nitrogen",	14.007,		elements);
			CreateNewElement(8,  "O",  "Oxygen",	15.999,		elements);
			CreateNewElement(9,  "F",  "Fluorine",	18.9984032, elements);
			CreateNewElement(10, "Ne", "Neon",		20.1797,	elements);

			///
			/// Elements 11 - 20
			/// 
			CreateNewElement(11, "Na", "Sodium",		22.98976928,	elements);
			CreateNewElement(12, "Mg", "Magnesium",		24.305,			elements);
			CreateNewElement(13, "Al", "Aluminium",		26.9815386,		elements); // The second i is officual, leaving it out is acceptalbe (according to the standards)
			CreateNewElement(14, "Si", "Silicon",		28.085,			elements);
			CreateNewElement(15, "P",  "Phosphorus",	30.973762,		elements);

			CreateNewElement(16, "S",  "Sulfur",		32.06,			elements);
			CreateNewElement(17, "Cl", "Chlorine",		35.45,			elements);
			CreateNewElement(18, "Ar", "Argon",			39.948,			elements);
			CreateNewElement(19, "K",  "Potassium",		39.0983,		elements);
			CreateNewElement(20, "Ca", "Calcium",		40.078,			elements);

			///
			/// Elements 21 - 30
			/// 
			CreateNewElement(21, "Sc", "Scandium",	44.955912,	elements);
			CreateNewElement(22, "Ti", "Titanium",	47.867,		elements);
			CreateNewElement(23, "V",  "Vanadium",	50.9415,	elements);
			CreateNewElement(24, "Cr", "Chromium",	51.9961,	elements);
			CreateNewElement(25, "Mn", "Manganese",	54.938045,	elements);
 
			CreateNewElement(26, "Fe", "Iron",		55.845,		elements);
			CreateNewElement(27, "Co", "Cobalt",	58.933195,	elements);
			CreateNewElement(28, "Ni", "Nickel",	58.6934,	elements);
			CreateNewElement(29, "Cu", "Copper",	63.546,		elements);
			CreateNewElement(30, "Zn", "Zinc",		65.38,		elements);

			///
			/// Elements 31 - 40
			/// 
			CreateNewElement(31, "Ga", "Gallium",	69.723,		elements);
			CreateNewElement(32, "Ge", "Germanium", 72.630,		elements);
			CreateNewElement(33, "As", "Arsenic",	74.92160,	elements);
			CreateNewElement(34, "Se", "Selenium",	78.96,		elements);
			CreateNewElement(35, "Br", "Bromine",	79.904,		elements);

			CreateNewElement(36, "Kr", "Krypton",	83.798,		elements);
			CreateNewElement(37, "Rb", "Rubidium",	85.4678,	elements);
			CreateNewElement(38, "Sr", "Strontium", 87.62,		elements);
			CreateNewElement(39, "Y",  "Yttrium",	88.90585,	elements);
			CreateNewElement(40, "Zr", "Zirconium", 91.224,		elements);

			///
			/// Elements 41 - 50
			/// 
			CreateNewElement(41, "Nb", "Niobium",		 92.90638,	elements);
			CreateNewElement(42, "Mo", "Molybdenum",	 95.96,		elements);
			CreateNewElement(43, "Tc", "Technetium",	 98,		elements);
			CreateNewElement(44, "Ru", "Ruthenium",		101.07,		elements);
			CreateNewElement(45, "Rh", "Rhodium",		102.90550,	elements);

			CreateNewElement(46, "Pd", "Palladium",		106.42,		elements);
			CreateNewElement(47, "Ag", "Silver",		107.8682,	elements);
			CreateNewElement(48, "Cd", "Cadmium",		112.411,	elements);
			CreateNewElement(49, "In", "Indium",		114.818,	elements);
			CreateNewElement(50, "Sn", "Tin",			118.710,	elements);

			///
			/// Elements 51 - 60
			/// 
			CreateNewElement(51, "Sb", "Antimony",		121.760,		elements);
			CreateNewElement(52, "Te", "Tellurium",		127.60,			elements);
			CreateNewElement(53, "I",  "Iodine",		126.90447,		elements);
			CreateNewElement(54, "Xe", "Xenon",			131.293,		elements);
			CreateNewElement(55, "Cs", "Caesium",		132.9054519,	elements);

			CreateNewElement(56, "Ba", "Barium",		137.327,		elements);
			CreateNewElement(57, "La", "Lanthanum",		138.90547,		elements);
			CreateNewElement(58, "Ce", "Cerium",		140.116,		elements);
			CreateNewElement(59, "Pr", "Praseodymium",	140.90765,		elements);
			CreateNewElement(60, "Nd", "Neodymium",		144.242,		elements);

			///
			/// Elements 61 - 70
			/// 
			CreateNewElement(61, "Pm", "Promethium",	145,		elements);
			CreateNewElement(62, "Sm", "Samarium",		150.36,		elements);
			CreateNewElement(63, "Eu", "Europium",		151.964,	elements);
			CreateNewElement(64, "Gd", "Gadolinium",	157.25,		elements);
			CreateNewElement(65, "Tb", "Terbium",		158.92535,	elements);

			CreateNewElement(66, "Dy", "Dysprosium",	162.5,		elements);
			CreateNewElement(67, "Ho", "Holmium",		164.93032,	elements);
			CreateNewElement(68, "Er", "Erbium",		167.259,	elements);
			CreateNewElement(69, "Tm", "Thulium",		168.93421,	elements);
			CreateNewElement(70, "Yb", "Ytterbium",		173.054,	elements);

			///
			/// Elements 71 - 80
			///
			CreateNewElement(71, "Lu", "Lutetium",	174.9668,	elements);
			CreateNewElement(72, "Hf", "Hafnium",	178.49,		elements);
			CreateNewElement(73, "Ta", "Tantalum",	180.94788,	elements);
			CreateNewElement(74, "W",  "Tungsten",	183.84,		elements);
			CreateNewElement(75, "Re", "Rhenium",	186.207,	elements);

			CreateNewElement(76, "Os", "Osmium",	190.23,		elements);
			CreateNewElement(77, "Ir", "Iridium",	192.217,	elements);
			CreateNewElement(78, "Pt", "Platinum",	195.084,	elements);
			CreateNewElement(79, "Au", "Gold",		196.966569, elements);
			CreateNewElement(80, "Hg", "Mercury",	200.592,	elements);

			///
			/// Elements 81 - 90
			/// 
			CreateNewElement(81, "Tl", "Thallium",	204.38,		elements);
			CreateNewElement(82, "Pb", "Lead",		207.2,		elements);
			CreateNewElement(83, "Bi", "Bismuth",	208.98040,	elements);
			CreateNewElement(84, "Po", "Polonium",	209,		elements);
			CreateNewElement(85, "At", "Astatine",	210,		elements);

			CreateNewElement(86, "Rn", "Radon",		222,		elements);
			CreateNewElement(87, "Fr", "Francium",	223,		elements);
			CreateNewElement(88, "Ra", "Radium",	226,		elements);
			CreateNewElement(89, "Ac", "Actinium",	227,		elements);
			CreateNewElement(90, "Th", "Thorium",	232.08306,	elements);

			///
			/// Elements 91 - 100
			/// 
			CreateNewElement(91,  "Pa", "Protactinium", 231.03588,	elements);
			CreateNewElement(92,  "U",  "Uranium",		238.02891,	elements);
			CreateNewElement(93,  "Np", "Neptunium",	237,		elements);
			CreateNewElement(94,  "Pu", "Plutonium",	244,		elements);
			CreateNewElement(95,  "Am", "Americium",	243,		elements);
								  
			CreateNewElement(96,  "Cm", "Curium",		247,		elements);
			CreateNewElement(97,  "Bk", "Berkelium",	247,		elements);
			CreateNewElement(98,  "Cf", "Californium",	251,		elements);
			CreateNewElement(99,  "Es", "Einsteinium",	252,		elements);
			CreateNewElement(100, "Fm", "Fermium",		257,		elements);

			///
			/// Elements 101 - 110
			/// 
			CreateNewElement(101, "Md", "Mendelevium",		258, elements);
			CreateNewElement(102, "No", "Nobelium",			259, elements);
			CreateNewElement(103, "Lr", "Lawrencium",		266, elements);
			CreateNewElement(104, "Rf", "Rutherfordium",	267, elements);
			CreateNewElement(105, "Db", "Dubnium",			268, elements);

			CreateNewElement(106, "Sg", "Seaborgium",		269, elements);
			CreateNewElement(107, "Bh", "Bohrium",			270, elements);
			CreateNewElement(108, "Hs", "Hassium",			269, elements);
			CreateNewElement(109, "Mt", "Meitnerium",		278, elements);
			CreateNewElement(110, "Ds", "Darmstadtium",		281, elements);

			///
			/// Elements 111 - 118
			/// 
			CreateNewElement(111, "Rg",  "Roetgenium",	281, elements);
			CreateNewElement(112, "Cn",  "Copernicium", 285, elements);
			CreateNewElement(113, "Uut", "Ununtrium",	186, elements);
			CreateNewElement(114, "Fl",  "Flerovium",	289, elements);
			CreateNewElement(115, "Uup", "Ununpentium", 288, elements);

			CreateNewElement(116, "Lv",  "Livermorium", 293, elements);
			CreateNewElement(117, "Uus", "Ununseptium", 294, elements);
			CreateNewElement(118, "Uuo", "Ununoctium",	294, elements);


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

			element.Checked += Element_Checked;
			element.Unchecked += Element_Unchecked;

			elements.Add(element);
		}
		#endregion
	}
}
