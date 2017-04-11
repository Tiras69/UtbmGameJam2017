using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("1")]
public class LoadAndSave {

    #region constructors
    public LoadAndSave()
    {
        ListCurrentLawId = new List<int>();

    }
    #endregion

    #region CurentLawsIds
    [XmlArray("2")]
    public List<int> ListCurrentLawId { get; set; }
    #endregion

    #region GovernementOpinion
    [XmlElement("3")]
    public int GovernementOpinion { get; set; }
    #endregion

    #region PopulaceOpinion
    [XmlElement("4")]
    public int PopulaceOpinion { get; set; }
    #endregion

    #region Economy
    [XmlElement("5")]
    public int EconomyValue { get; set; }
    #endregion

    #region Employment
    [XmlElement("6")]
    public int EmploymentValue { get; set; }
    #endregion

    #region Religion
    [XmlElement("7")]
    public int ReligionValue { get; set; }
    #endregion

    #region Money
    [XmlElement("8")]
    public int MoneyValue { get; set; }
    #endregion

    #region CurrentMonthInSemester
    [XmlElement("9")]
    public int CurrentMonthInSemester { get; set; }
    #endregion

    #region CurrentLawId
    [XmlElement("10")]
    public int CurrentLawId { get; set; }
    #endregion


    [XmlRoot("11")]
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

        [XmlAttribute("12")]
        public GameProperty Property { get; set; }

        [XmlAttribute("13")]
        public int Value { get; set; }
    }
}


