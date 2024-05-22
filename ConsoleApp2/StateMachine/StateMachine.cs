using System;
using System.Collections.Generic;

public class StateMachine
{
    private State currentState;
    private List<State> states;
    private List<transition> transitions;

    public StateMachine()
    {
        // Initialize the list of states
        states = new List<State>();

        // Initialize the list of transitions
        transitions = new List<transition>();
    }

    public void AddState(State state)
    {
        states.Add(state);
    }

    public void AddTransition(transition transition)
    {
        transitions.Add(transition);
    }

    public void PerformAction(Action action)
    {

    }  
}
