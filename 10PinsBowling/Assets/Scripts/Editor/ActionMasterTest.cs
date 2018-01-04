using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ActionMasterTest {
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;

	[Test]
	public void T00PassingTest() {
        Assert.AreEqual(1, 1);
	}

	[Test]
    public void T01OnStrikeReturnEndTurn () {
        ActionMaster actionMaster = new ActionMaster();
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }
}
