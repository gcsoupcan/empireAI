//////////////////////////////////////////////////////////////////////////////////////////
//This Source Code File Is Part Of An
//AI DLL Assembly for 
//Empire Deluxe Combined Edition
//
//Copyright 2017 Mark Kinkead
//May be freely hacked and altered to make your own 
//non-commercial AI DLLs for Empire Deluxe Combined Edition
//All other rights reserved
//
//This code can be hacked to to build AI players
//Please remeber to create a unique dll name for your player
//in order to be recognized by the game
//This code can only be worked in association with 
//the game Empire Deluxe Combined Edition
//
//Version Release Information Available
//In the Factory file of each AI player
//
//
////////////////////////////////////////////////////////////////////////////////////////


//This is the implementation of the AI Factory for the Example AI.
//It should not be within any namespace in order to be
//recognized by the game program.

using System.Collections.Generic;
using com.kbs.empire.ai.common.player;
using com.kbs.empire.ai.common.proc;
using com.kbs.empire.ai.example;
using com.kbs.empire.common.game.data;
using com.kbs.empire.common.game.hints;
using com.kbs.empire.common.util.random;
using com.kbs.empire.common.util.xml;

//VERSION 1 09-09-17
public class ExampleAI : AIPlayerFactory
{
    ////////////////////////////////////////////
    //For Individual Name Selection
    private static readonly string[] leaders_ =
        {
            "Example"
        };

    private readonly List<string> aiGroup_ = new List<string>();
    private readonly CMTRandom random_ = new CMTRandom();
    ////////////////////////////////////////////


    ////////////////////////////////////////////
    //The returned list gives the Unit Set DB keys
    //that the AI is most attuned to. This can 
    //include strings representing the keys for modified databases
    public override List<string> bestBuildSets()
    {
        var ret = new List<string>();
        ret.Add(EmpireCC.US_BS);
        ret.Add(EmpireCC.US_SS);
        ret.Add(EmpireCC.US_AS);
        ret.Add(EmpireCC.US_ES);
        return ret;
    }

    ////////////////////////////////////////////
    //Hint Mechanism
    //This allows the factory to serve up a new 
    //hint set for the player to configure
    public override CDLLHints getHints()
    {
        return ExamplePlayer.getHints();
    }


    ////////////////////////////////////////////
    //Creation Call 
    //Returns a new instance of the player.
    //note the pplayer name is also being set here
    public override AIPlayer createAIPlayer(int position, string logpath, string logname, CDLLHints hints,
        AIEventInterfaceI aiEvent, AICommandInterfaceI command, AIQueryI query, AICheatI cheat, int logLevel) 
    {
        //name selection
        if (aiGroup_.Count == 0)
        {
            for (int i = 0; i < leaders_.Length; i++)
                aiGroup_.Add(leaders_[i]);
        }

        int r = random_.nextInt(aiGroup_.Count);
        string pname = aiGroup_[r];
        aiGroup_.RemoveAt(r);

        //instance creation
        return new ExamplePlayer(position, pname, logpath, logname, hints, aiEvent, command, query, cheat, logLevel);
    }

    ////////////////////////////////////////////
    //Reload Call
    //This call gives the data needed to reload an AI player instance
    public override AIPlayer reloadAIPlayer(int position, CEncodedObjectInputBufferI bin, string logpath, string logname,
        AIEventInterfaceI aiEvent, AICommandInterfaceI command, AIQueryI query, AICheatI cheat, int logLevel) 
    {
        Dictionary<string, string> caMap = bin.getAttributes();
        return new ExamplePlayer(position, caMap, bin, logpath, logname, aiEvent, command, query, cheat, logLevel);
    }


}
