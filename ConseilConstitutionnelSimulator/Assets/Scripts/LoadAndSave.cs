using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("LoadAndSave")]
public class LoadAndSave {

    #region constructors
    public LoadAndSave()
    {
        ListCurrentLawId = new List<int>();

    }
    #endregion

    #region CurentLawsIds
    [XmlArray("ListCurrentLawId")]
    public List<int> ListCurrentLawId { get; set; }
    #endregion

    #region GovernementOpinion
    [XmlElement("GovernementOpinion")]
    public int GovernementOpinion { get; set; }
    #endregion

    #region PopulaceOpinion
    [XmlElement("PopulaceOpinion")]
    public int PopulaceOpinion { get; set; }
    #endregion

    #region Economy
    [XmlElement("Economy")]
    public int EconomyValue { get; set; }
    #endregion

    #region Employment
    [XmlElement("Employment")]
    public int EmploymentValue { get; set; }
    #endregion

    #region Religion
    [XmlElement("Religion")]
    public int ReligionValue { get; set; }
    #endregion

    #region Money
    [XmlElement("Money")]
    public int MoneyValue { get; set; }
    #endregion

    #region CurrentMonthInSemester
    [XmlElement("CurrentMonthInSemester")]
    public int CurrentMonthInSemester { get; set; }
    #endregion

    #region CurrentLawId
    [XmlElement("CurrentLawId")]
    public int CurrentLawId { get; set; }
    #endregion


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
}


