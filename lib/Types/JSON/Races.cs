using Newtonsoft.Json;

namespace NetBot.Lib.Types.JSON
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class _3
    {
        public _3(Daily daily)
        {
            this.daily = daily;
        }

        public Daily daily { get; set; }
    }

    public class _5
    {
        public _5(Daily daily)
        {
            this.daily = daily;
        }
        public Daily daily { get; set; }
    }

    public class Ability
    {
        public Ability(int dex, int wis, int? cha, Choose choose, int? str, int? @int, int? con)
        {
            this.dex = dex;
            this.wis = wis;
            this.cha = cha;
            this.choose = choose;
            this.str = str;
            this.@int = @int;
            this.con = con;
        }

        public int dex { get; set; }
        public int wis { get; set; }
        public int? cha { get; set; }
        public Choose choose { get; set; }
        public int? str { get; set; }
        public int? @int { get; set; }
        public int? con { get; set; }
    }

    public class AdditionalSource
    {
        public AdditionalSource(string source, int page)
        {
            this.source = source;
            this.page = page;
        }

        public string source { get; set; }
        public int page { get; set; }
    }

    public class AdditionalSpell
    {
        public AdditionalSpell(Innate innate, object ability, Known known, Expanded expanded)
        {
            this.innate = innate;
            this.ability = ability;
            this.known = known;
            this.expanded = expanded;
        }

        public Innate innate { get; set; }
        public object ability { get; set; }
        public Known known { get; set; }
        public Expanded expanded { get; set; }
    }

    public class Age
    {
        public Age(int mature, int max)
        {
            this.mature = mature;
            this.max = max;
        }

        public int mature { get; set; }
        public int max { get; set; }
    }

    public class ArmorProficiency
    {
        public ArmorProficiency(bool light, bool medium)
        {
            this.light = light;
            this.medium = medium;
        }

        public bool light { get; set; }
        public bool medium { get; set; }
    }

    public class Choose
    {
        public Choose(List<string> from, int count, int? amount)
        {
            this.from = from;
            this.count = count;
            this.amount = amount;
        }

        public List<string> from { get; set; }
        public int count { get; set; }
        public int? amount { get; set; }
    }

    public class Copy
    {
        public Copy(string name, string source, Mod mod)
        {
            this.name = name;
            this.source = source;
            _mod = mod;
        }

        public string name { get; set; }
        public string source { get; set; }
        public Mod _mod { get; set; }
    }

    public class Daily
    {
        public Daily(List<string> _1)
        {
            this._1 = _1;
        }

        [JsonProperty("1")]
        public List<string> _1 { get; set; }
    }

    public class Entry
    {
        public Entry(string mode, string replace, Items items, string names)
        {
            this.mode = mode;
            this.replace = replace;
            this.items = items;
            this.names = names;
        }

        public string mode { get; set; }
        public string replace { get; set; }
        public Items items { get; set; }
        public string names { get; set; }
    }

    public class Expanded
    {
        public Expanded(List<string> s1, List<string> s2, List<string> s3, List<string> s4, List<string> s5)
        {
            this.s1 = s1;
            this.s2 = s2;
            this.s3 = s3;
            this.s4 = s4;
            this.s5 = s5;
        }

        public List<string> s1 { get; set; }
        public List<string> s2 { get; set; }
        public List<string> s3 { get; set; }
        public List<string> s4 { get; set; }
        public List<string> s5 { get; set; }
    }

    public class Feat
    {
        public Feat(int any)
        {
            this.any = any;
        }

        public int any { get; set; }
    }

    public class HeightAndWeight
    {
        public HeightAndWeight(int baseHeight, string heightMod, int baseWeight, string weightMod)
        {
            this.baseHeight = baseHeight;
            this.heightMod = heightMod;
            this.baseWeight = baseWeight;
            this.weightMod = weightMod;
        }

        public int baseHeight { get; set; }
        public string heightMod { get; set; }
        public int baseWeight { get; set; }
        public string weightMod { get; set; }
    }

    public class Implementation
    {
        public Implementation(Variables variables, List<string> resist)
        {
            _variables = variables;
            this.resist = resist;
        }

        public Variables _variables { get; set; }
        public List<string> resist { get; set; }
    }

    public class Innate
    {
        public Innate(object _3, _5 _5, object _1)
        {
            this._3 = _3;
            this._5 = _5;
            this._1 = _1;
        }

        [JsonProperty("3")]
        public object _3 { get; set; }

        [JsonProperty("5")]
        public _5 _5 { get; set; }

        [JsonProperty("1")]
        public object _1 { get; set; }
    }

    public class Items
    {
        public Items(string name, string type, List<string> entries)
        {
            this.name = name;
            this.type = type;
            this.entries = entries;
        }

        public string name { get; set; }
        public string type { get; set; }
        public List<string> entries { get; set; }
    }

    public class Known
    {
        public Known(object _1)
        {
            this._1 = _1;
        }

        [JsonProperty("1")]
        public object _1 { get; set; }
    }

    public class LanguageProficiency
    {
        public LanguageProficiency(bool auran, bool? common, bool? other, bool? celestial, int? anyStandard, bool? goblin, bool? sylvan, bool? draconic, bool? dwarvish, bool? elvish, bool? giant, bool? primordial, bool undercommon, bool? aquan, bool? gnomish)
        {
            this.auran = auran;
            this.common = common;
            this.other = other;
            this.celestial = celestial;
            this.anyStandard = anyStandard;
            this.goblin = goblin;
            this.sylvan = sylvan;
            this.draconic = draconic;
            this.dwarvish = dwarvish;
            this.elvish = elvish;
            this.giant = giant;
            this.primordial = primordial;
            this.undercommon = undercommon;
            this.aquan = aquan;
            this.gnomish = gnomish;
        }

        public bool auran { get; set; }
        public bool? common { get; set; }
        public bool? other { get; set; }
        public bool? celestial { get; set; }
        public int? anyStandard { get; set; }
        public bool? goblin { get; set; }
        public bool? sylvan { get; set; }
        public bool? draconic { get; set; }
        public bool? dwarvish { get; set; }
        public bool? elvish { get; set; }
        public bool? giant { get; set; }
        public bool? primordial { get; set; }
        public bool undercommon { get; set; }
        public bool? aquan { get; set; }
        public bool? gnomish { get; set; }
    }

    public class Meta
    {
        public Meta(List<string> internalCopies)
        {
            this.internalCopies = internalCopies;
        }

        public List<string> internalCopies { get; set; }
    }

    public class Mod
    {
        public Mod(List<Entry> entries)
        {
            this.entries = entries;
        }

        public List<Entry> entries { get; set; }
    }

    public class OtherSource
    {
        public OtherSource(string source, int page)
        {
            this.source = source;
            this.page = page;
        }

        public string source { get; set; }
        public int page { get; set; }
    }

    public class Overwrite
    {
        public Overwrite(bool ability, bool traitTags, bool? languageProficiencies)
        {
            this.ability = ability;
            this.traitTags = traitTags;
            this.languageProficiencies = languageProficiencies;
        }

        public bool ability { get; set; }
        public bool traitTags { get; set; }
        public bool? languageProficiencies { get; set; }
    }

    public class Race
    {
        public Race(string name, string source, int page, List<string> size, object speed, List<Ability> ability, List<string> traitTags, List<LanguageProficiency> languageProficiencies, List<object> entries, List<OtherSource> otherSources, List<string> reprintedAs, Age age, SoundClip soundClip, bool? hasFluff, bool? hasFluffImages, object lineage, List<AdditionalSpell> additionalSpells, int? darkvision, List<object> resist, List<Version> versions, HeightAndWeight heightAndWeight, List<SkillProficiency> skillProficiencies, List<object> creatureTypes, List<string> creatureTypeTags, List<ToolProficiency> toolProficiencies, List<string> conditionImmune, Copy copy, List<Feat> feats, bool? srd, bool? basicRules, List<WeaponProficiency> weaponProficiencies, List<AdditionalSource> additionalSources)
        {
            this.name = name;
            this.source = source;
            this.page = page;
            this.size = size;
            this.speed = speed;
            this.ability = ability;
            this.traitTags = traitTags;
            this.languageProficiencies = languageProficiencies;
            this.entries = entries;
            this.otherSources = otherSources;
            this.reprintedAs = reprintedAs;
            this.age = age;
            this.soundClip = soundClip;
            this.hasFluff = hasFluff;
            this.hasFluffImages = hasFluffImages;
            this.lineage = lineage;
            this.additionalSpells = additionalSpells;
            this.darkvision = darkvision;
            this.resist = resist;
            _versions = versions;
            this.heightAndWeight = heightAndWeight;
            this.skillProficiencies = skillProficiencies;
            this.creatureTypes = creatureTypes;
            this.creatureTypeTags = creatureTypeTags;
            this.toolProficiencies = toolProficiencies;
            this.conditionImmune = conditionImmune;
            _copy = copy;
            this.feats = feats;
            this.srd = srd;
            this.basicRules = basicRules;
            this.weaponProficiencies = weaponProficiencies;
            this.additionalSources = additionalSources;
        }

        public string name { get; set; }
        public string source { get; set; }
        public int page { get; set; }
        public List<string> size { get; set; }
        public object speed { get; set; }
        public List<Ability> ability { get; set; }
        public List<string> traitTags { get; set; }
        public List<LanguageProficiency> languageProficiencies { get; set; }
        public List<object> entries { get; set; }
        public List<OtherSource> otherSources { get; set; }
        public List<string> reprintedAs { get; set; }
        public Age age { get; set; }
        public SoundClip soundClip { get; set; }
        public bool? hasFluff { get; set; }
        public bool? hasFluffImages { get; set; }
        public object lineage { get; set; }
        public List<AdditionalSpell> additionalSpells { get; set; }
        public int? darkvision { get; set; }
        public List<object> resist { get; set; }
        public List<Version> _versions { get; set; }
        public HeightAndWeight heightAndWeight { get; set; }
        public List<SkillProficiency> skillProficiencies { get; set; }
        public List<object> creatureTypes { get; set; }
        public List<string> creatureTypeTags { get; set; }
        public List<ToolProficiency> toolProficiencies { get; set; }
        public List<string> conditionImmune { get; set; }
        public Copy _copy { get; set; }
        public List<Feat> feats { get; set; }
        public bool? srd { get; set; }
        public bool? basicRules { get; set; }
        public List<WeaponProficiency> weaponProficiencies { get; set; }
        public List<AdditionalSource> additionalSources { get; set; }
    }

    public class Root
    {
        public Root(Meta meta, List<Race> race, List<Subrace> subrace)
        {
            _meta = meta;
            this.race = race;
            this.subrace = subrace;
        }

        public Meta _meta { get; set; }
        public List<Race> race { get; set; }
        public List<Subrace> subrace { get; set; }
    }

    public class SkillProficiency
    {
        public SkillProficiency(bool intimidation, bool? perception, bool? stealth, Choose choose, bool? survival, bool? deception, int? any)
        {
            this.intimidation = intimidation;
            this.perception = perception;
            this.stealth = stealth;
            this.choose = choose;
            this.survival = survival;
            this.deception = deception;
            this.any = any;
        }

        public bool intimidation { get; set; }
        public bool? perception { get; set; }
        public bool? stealth { get; set; }
        public Choose choose { get; set; }
        public bool? survival { get; set; }
        public bool? deception { get; set; }
        public int? any { get; set; }
    }

    public class SkillToolLanguageProficiency
    {
        public SkillToolLanguageProficiency(List<Choose> choose)
        {
            this.choose = choose;
        }

        public List<Choose> choose { get; set; }
    }

    public class SoundClip
    {
        public SoundClip(string type, string path)
        {
            this.type = type;
            this.path = path;
        }

        public string type { get; set; }
        public string path { get; set; }
    }

    public class Subrace
    {
        public Subrace(string name, string source, string raceName, string raceSource, int page, List<Ability> ability, List<object> entries, bool hasFluff, bool hasFluffImages, List<SkillProficiency> skillProficiencies, bool? srd, List<Version> versions, int? darkvision, List<string> resist, Overwrite overwrite, List<OtherSource> otherSources, List<string> reprintedAs, List<string> traitTags, List<LanguageProficiency> languageProficiencies, List<AdditionalSpell> additionalSpells, bool? basicRules, HeightAndWeight heightAndWeight, List<ArmorProficiency> armorProficiencies, object speed, List<string> alias, List<WeaponProficiency> weaponProficiencies, List<SkillToolLanguageProficiency> skillToolLanguageProficiencies, Age age)
        {
            this.name = name;
            this.source = source;
            this.raceName = raceName;
            this.raceSource = raceSource;
            this.page = page;
            this.ability = ability;
            this.entries = entries;
            this.hasFluff = hasFluff;
            this.hasFluffImages = hasFluffImages;
            this.skillProficiencies = skillProficiencies;
            this.srd = srd;
            _versions = versions;
            this.darkvision = darkvision;
            this.resist = resist;
            this.overwrite = overwrite;
            this.otherSources = otherSources;
            this.reprintedAs = reprintedAs;
            this.traitTags = traitTags;
            this.languageProficiencies = languageProficiencies;
            this.additionalSpells = additionalSpells;
            this.basicRules = basicRules;
            this.heightAndWeight = heightAndWeight;
            this.armorProficiencies = armorProficiencies;
            this.speed = speed;
            this.alias = alias;
            this.weaponProficiencies = weaponProficiencies;
            this.skillToolLanguageProficiencies = skillToolLanguageProficiencies;
            this.age = age;
        }

        public string name { get; set; }
        public string source { get; set; }
        public string raceName { get; set; }
        public string raceSource { get; set; }
        public int page { get; set; }
        public List<Ability> ability { get; set; }
        public List<object> entries { get; set; }
        public bool hasFluff { get; set; }
        public bool hasFluffImages { get; set; }
        public List<SkillProficiency> skillProficiencies { get; set; }
        public bool? srd { get; set; }
        public List<Version> _versions { get; set; }
        public int? darkvision { get; set; }
        public List<string> resist { get; set; }
        public Overwrite overwrite { get; set; }
        public List<OtherSource> otherSources { get; set; }
        public List<string> reprintedAs { get; set; }
        public List<string> traitTags { get; set; }
        public List<LanguageProficiency> languageProficiencies { get; set; }
        public List<AdditionalSpell> additionalSpells { get; set; }
        public bool? basicRules { get; set; }
        public HeightAndWeight heightAndWeight { get; set; }
        public List<ArmorProficiency> armorProficiencies { get; set; }
        public object speed { get; set; }
        public List<string> alias { get; set; }
        public List<WeaponProficiency> weaponProficiencies { get; set; }
        public List<SkillToolLanguageProficiency> skillToolLanguageProficiencies { get; set; }
        public Age age { get; set; }
    }

    public class Template
    {
        public Template(string name, string source, Mod mod)
        {
            this.name = name;
            this.source = source;
            _mod = mod;
        }

        public string name { get; set; }
        public string source { get; set; }
        public Mod _mod { get; set; }
    }

    public class ToolProficiency
    {
        public ToolProficiency(int any)
        {
            this.any = any;
        }

        public int any { get; set; }
    }

    public class Variables
    {
        public Variables(string color, string damageType, string area, string savingThrow)
        {
            this.color = color;
            this.damageType = damageType;
            this.area = area;
            this.savingThrow = savingThrow;
        }

        public string color { get; set; }
        public string damageType { get; set; }
        public string area { get; set; }
        public string savingThrow { get; set; }
    }

    public class Version
    {
        public Version(Mod mod, object skillProficiencies, object darkvision, Template template, List<Implementation> implementations, string name = "N/A", string source = "N/A")
        {
            this.name = name;
            this.source = source;
            _mod = mod;
            this.skillProficiencies = skillProficiencies;
            this.darkvision = darkvision;
            _template = template;
            _implementations = implementations;
        }

        public string name { get; set; }
        public string source { get; set; }
        public Mod _mod { get; set; }
        public object skillProficiencies { get; set; }
        public object darkvision { get; set; }
        public Template _template { get; set; }
        public List<Implementation> _implementations { get; set; }
    }

    public class WeaponProficiency
    {
        public WeaponProficiency(bool battleaxephb, bool handaxephb, bool lighthammerphb, bool warhammerphb, bool? longswordphb, bool? shortswordphb, bool? shortbowphb, bool? longbowphb, bool rapierphb, bool handcrossbowphb, bool? spearphb, bool? netphb, bool? scimitarphb, bool? doublebladedscimitarerlw, bool? tridentphb, bool? lightcrossbowphb, bool? greatswordphb)
        {
            this.battleaxephb = battleaxephb;
            this.handaxephb = handaxephb;
            this.lighthammerphb = lighthammerphb;
            this.warhammerphb = warhammerphb;
            this.longswordphb = longswordphb;
            this.shortswordphb = shortswordphb;
            this.shortbowphb = shortbowphb;
            this.longbowphb = longbowphb;
            this.rapierphb = rapierphb;
            this.handcrossbowphb = handcrossbowphb;
            this.spearphb = spearphb;
            this.netphb = netphb;
            this.scimitarphb = scimitarphb;
            this.doublebladedscimitarerlw = doublebladedscimitarerlw;
            this.tridentphb = tridentphb;
            this.lightcrossbowphb = lightcrossbowphb;
            this.greatswordphb = greatswordphb;
        }

        [JsonProperty("battleaxe|phb")]
        public bool battleaxephb { get; set; }

        [JsonProperty("handaxe|phb")]
        public bool handaxephb { get; set; }

        [JsonProperty("light hammer|phb")]
        public bool lighthammerphb { get; set; }

        [JsonProperty("warhammer|phb")]
        public bool warhammerphb { get; set; }

        [JsonProperty("longsword|phb")]
        public bool? longswordphb { get; set; }

        [JsonProperty("shortsword|phb")]
        public bool? shortswordphb { get; set; }

        [JsonProperty("shortbow|phb")]
        public bool? shortbowphb { get; set; }

        [JsonProperty("longbow|phb")]
        public bool? longbowphb { get; set; }

        [JsonProperty("rapier|phb")]
        public bool rapierphb { get; set; }

        [JsonProperty("hand crossbow|phb")]
        public bool handcrossbowphb { get; set; }

        [JsonProperty("spear|phb")]
        public bool? spearphb { get; set; }

        [JsonProperty("net|phb")]
        public bool? netphb { get; set; }

        [JsonProperty("scimitar|phb")]
        public bool? scimitarphb { get; set; }

        [JsonProperty("double-bladed scimitar|erlw")]
        public bool? doublebladedscimitarerlw { get; set; }

        [JsonProperty("trident|phb")]
        public bool? tridentphb { get; set; }

        [JsonProperty("light crossbow|phb")]
        public bool? lightcrossbowphb { get; set; }

        [JsonProperty("greatsword|phb")]
        public bool? greatswordphb { get; set; }
    }


}