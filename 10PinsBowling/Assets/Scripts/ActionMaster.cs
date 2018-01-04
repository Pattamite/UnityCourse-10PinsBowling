using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster {
    private int[] bowls = new int[21];
    private int bowl = 0;

    public enum Action {
        Tidy,
        Reset,
        EndTurn,
        EndGame,
    };

    public static Action NextAction (List<int> pinFalls) {
        ActionMaster am = new ActionMaster();
        Action currentAction = new Action();

        foreach(int pinFall in pinFalls) {
            currentAction = am.Bowl(pinFall);
        }

        return currentAction;
    }

	private Action Bowl (int pins) {
        if (pins < 0 || pins > 10) {
            throw new UnityException("Invalid pins value");
        }

        bowls[bowl] = pins;

        if (bowl == 20 || (bowl == 19 && !Bowl21Awarded())) {
            return Action.EndGame;
        }
        else if(bowl == 19 && bowls[18] == 10) {
            bowl += 1;
            if(pins == 10) {
                return Action.Reset;
            }
            else {
                return Action.Tidy;
            }
        }
        else if (bowl >= 18 && Bowl21Awarded()) {
            bowl += 1;
            return Action.Reset;
        }


        if (bowl % 2 == 0) {
            if(pins == 10) {
                bowl += 2;
                return Action.EndTurn;
            }
            else {
                bowl += 1;
                return Action.Tidy;
            }
        }
        else if (bowl % 2 != 0) {
            bowl += 1;
            return Action.EndTurn;
        }

        throw new UnityException("Not sure what Action to return.");
    }

    private bool Bowl21Awarded () {
        return (bowls[18] + bowls[19]) >= 10;
    }
}
