using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSerialize : MonoBehaviour {

	// Use this for initialization
	void Start () {

        SerializeTest();
	}
	
    private void SerializeTest()
    {
        Law law = new Law();
        law.Title = "Titre 1";
        law.Description = "Description Lorem Ipsum";

        law.YesPropertyModifiers.Add(new PropertyModifier(GameProperty.GameProperty_ECONOMY, 10));
        law.YesPropertyModifiers.Add(new PropertyModifier(GameProperty.GameProperty_EMPLOYMENT, 9));
        law.YesPropertyModifiers.Add(new PropertyModifier(GameProperty.GameProperty_GOVERNMENTOPINION, 53));

        law.YesLawsToAdd.Add(3);
        law.YesLawsToAdd.Add(8);

        law.NoPropertyModifiers.Add(new PropertyModifier(GameProperty.GameProperty_PERSONALMONEY, 1000));
        law.NoPropertyModifiers.Add(new PropertyModifier(GameProperty.GameProperty_POPULACEOPINION, -90));
        law.NoPropertyModifiers.Add(new PropertyModifier(GameProperty.GameProperty_RELIGION, 8));

        law.NoLawsToAdd.Add(5);
        law.NoLawsToAdd.Add(8);

        

        
        law.MinimizeLawsToAdd.Add(1);
        law.MinimizePropertyModifiers.Add(new PropertyModifier(GameProperty.GameProperty_GOVERNMENTOPINION, -60));

        XmlSerializerHelper<Law>.SerializeXmlFile("loi1.xml", law);
    }

    private void Deserialize()
    {
        Law law;
        law = XmlSerializerHelper<Law>.DeserializeXmlFile("Test.xml");
        Debug.Log("coucou");
    }

	// Update is called once per frame
	void Update () {
		
	}
}
