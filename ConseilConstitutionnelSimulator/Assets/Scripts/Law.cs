using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[Flags]
public enum GameProperty
{
    [XmlEnum("GOVERNMENTOPINION")]
    GameProperty_GOVERNMENTOPINION = 0,
    [XmlEnum("POPULACEOPINION")]
    GameProperty_POPULACEOPINION = 1,
    [XmlEnum("PERSONALMONEY")]
    GameProperty_PERSONALMONEY = 2,
    [XmlEnum("ECONOMY")]
    GameProperty_ECONOMY = 3,
    [XmlEnum("EMPLOYMENT")]
    GameProperty_EMPLOYMENT = 4,
    [XmlEnum("RELIGION")]
    GameProperty_RELIGION = 5
};

[XmlRoot("Law")]
public class Law {

    #region constructors
    public Law()
    {
        YesPropertyModifiers = new List<PropertyModifier>();
        YesLawsToAdd = new List<int>();

        NoPropertyModifiers = new List<PropertyModifier>();
        NoLawsToAdd = new List<int>();

        MaximizePropertyModifiers = new List<PropertyModifier>();
        MaximizeLawsToAdd = new List<int>();

        MinimizePropertyModifiers = new List<PropertyModifier>();
        MinimizeLawsToAdd = new List<int>();
    }
    #endregion

    #region Global Values
    [XmlAttribute("Id")]
    public int Id { get; set; }

    [XmlElement("Title")]
    public string Title { get; set; }

    [XmlElement("Description")]
    public string Description { get; set; }
    #endregion

    #region Yes Data
    [XmlElement("YesSentence")]
    public string YesSentence { get; set; }

    [XmlArray("YesPropertyModifiers")]
    [XmlArrayItem("PropertyModifier", typeof(PropertyModifier))]
    public List<PropertyModifier> YesPropertyModifiers { get; set; }

    [XmlArray("YesLawsToAdd")]
    [XmlArrayItem("LawId", typeof(int))]
    public List<int> YesLawsToAdd { get; set; }
    #endregion

    #region No Data
    [XmlElement("NoSentence")]
    public string NoSentence { get; set; }

    [XmlArray("NoPropertyModifiers")]
    [XmlArrayItem("PropertyModifier", typeof(PropertyModifier))]
    public List<PropertyModifier> NoPropertyModifiers { get; set; }

    [XmlArray("NoLawsToAdd")]
    [XmlArrayItem("LawId", typeof(int))]
    public List<int> NoLawsToAdd { get; set; }
    #endregion

    #region Minimize Data
    [XmlElement("MinimizeSentence")]
    public string MinimizeSentence { get; set; }

    [XmlArray("MinimizePropertyModifiers")]
    [XmlArrayItem("PropertyModifier", typeof(PropertyModifier))]
    public List<PropertyModifier> MinimizePropertyModifiers { get; set; }

    [XmlArray("MinimizeLawsToAdd")]
    [XmlArrayItem("LawId", typeof(int))]
    public List<int> MinimizeLawsToAdd { get; set; }
    #endregion

    #region Maximize Data
    [XmlElement("MaximizeSentence")]
    public string MaximizeSentence { get; set; }

    [XmlArray("MaximizePropertyModifiers")]
    [XmlArrayItem("PropertyModifier", typeof(PropertyModifier))]
    public List<PropertyModifier> MaximizePropertyModifiers { get; set; }

    [XmlArray("MaximizeLawsToAdd")]
    [XmlArrayItem("LawId", typeof(int))]
    public List<int> MaximizeLawsToAdd { get; set; }
    #endregion
}

[XmlRoot("PropertyModifier")]
public class PropertyModifier
{
    public PropertyModifier()
    {
        
    }

    public PropertyModifier(GameProperty _property, int _value)
    {
        Property = _property;
        Value = _value;
    }

    [XmlAttribute("AttributeName")]
    public GameProperty Property { get; set; }

    [XmlAttribute("Value")]
    public int Value { get; set; }
}