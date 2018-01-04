using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ActionMasterTest {
    private ActionMaster actionMaster;
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;

    [SetUp]
    public void Setup () {
        actionMaster = new ActionMaster();
    }

    [Test]
	public void T00PassingTest() {
        Assert.AreEqual(1, 1);
	}

	[Test]
    public void T01OnStrikeReturnEndTurn () {
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }

    [Test]
    public void T02Bowl8ReturnsTidy () {
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
    }

    [Test]
    public void T03TwoBowlReturnTidyAndEndTurn () {
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
        Assert.AreEqual(endTurn, actionMaster.Bowl(2));
    }

    [Test]
    public void T04CheckResetAtStrikeInLastFrame () {
        int[] rolls = {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
        foreach(int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void T05CheckResetAtSpareInLastFrame () {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(1));
        Assert.AreEqual(reset, actionMaster.Bowl(9));
    }

    [Test]
    public void T06EndGame () {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(1));
        Assert.AreEqual(endGame, actionMaster.Bowl(8));
    }

    [Test]
    public void T07EndGameWith21 () {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(1));
        Assert.AreEqual(reset, actionMaster.Bowl(9));
        Assert.AreEqual(endGame, actionMaster.Bowl(9));
    }

    [Test]
    public void T08EndGameWith21_2 () {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(endGame, actionMaster.Bowl(9));
    }

    [Test]
    public void T09EndGameWith21AndStrike () {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(tidy, actionMaster.Bowl(9));
        Assert.AreEqual(endGame, actionMaster.Bowl(1));
    }

    [Test]
    public void T10EndGameWith21AndStrike_2 () {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
        Assert.AreEqual(endGame, actionMaster.Bowl(1));
    }

    [Test]
    public void T11Spare10 () {
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
        Assert.AreEqual(tidy, actionMaster.Bowl(1));
    }

    [Test]
    public void T12Strike () {
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
        Assert.AreEqual(endTurn, actionMaster.Bowl(2));
        Assert.AreEqual(tidy, actionMaster.Bowl(1));
    }

    [Test]
    public void T13Turkey () {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }
}
