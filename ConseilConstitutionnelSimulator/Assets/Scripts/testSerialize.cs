using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSerialize : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Deserialize();

	}
	
    private void SerializeTest()
    {
        Law law = new Law();
        law.Title = "coucou test title";
        law.Description = "Description incroyable";
        law.YesPropertyModifiers.Add(new PropertyModifier(GameProperty.GameProperty_ECONOMY, 10));
        law.NoPropertyModifiers.Add(new PropertyModifier(GameProperty.GameProperty_GOVERNMENTOPINION, 10));
        law.MaximizeLawsToAdd.Add(5);
        law.MinimizeLawsToAdd.Add(1);

        XmlSerializerHelper<Law>.SerializeXmlFile("Test.xml", law);
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
