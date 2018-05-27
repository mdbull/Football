using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Football.Engine
{
    

    /// <summary>
    /// 
    /// </summary>
    public class PassPlay:Play
    {
        protected Player thrower = null;
        protected Player[] availableReceivers = null;
      
        protected PassPlayStatSheet gamePassStats = null;
        protected CarryStatSheet gameRunStats = null;
        protected CarryStatSheet gameReceiverStats = null;

        /// <summary>
        /// 
        /// </summary>
        public PassPlay():base(0) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cc"></param>
        /// <param name="offense"></param>
        /// <param name="defense"></param>
        /// <param name="gamePassStats"></param>
        /// <param name="gameRunStats"></param>
        /// <param name="gameReceiverStats"></param>
        public PassPlay(GameStats gameStats,ChainCrew cc)
            : base(cc, cc.TeamInPossession, cc.TeamNotInPossession)
        {
            Team tip = this.offense;
            Team tnip = this.defense;
            this.thrower = tip.TeamOffense.GetPlayerAtPosition("QB");
            this.availableReceivers = tip.TeamOffense.AvailableReceivers;
            this.principalBallcarrier = SelectReceiver(tip.TeamOffense.AvailableReceivers[0], tip.TeamOffense.AvailableReceivers);

            int throwerIndex = tip.GetIndexOfPlayer(this.thrower);
            int receiverIndex = tip.TeamOffense.GetAvailableReceiverAtIndex(this.principalBallcarrier);
           
            this.gamePassStats = (PassPlayStatSheet)gameStats[tip,throwerIndex][StatTypes.Pass];
            this.gameRunStats = (CarryStatSheet)gameStats[tip,throwerIndex][StatTypes.Run];
            this.gameReceiverStats = (CarryStatSheet)gameStats[tip,receiverIndex][StatTypes.Receive];
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="primaryReceiver">Player</param>
        /// <returns>Player</returns>
        private Player SelectReceiver(Player primaryReceiver,params Player[] receivers)
        {
            int check = Dice.Roll("d6");
            if (check < 3)//1 or 2 - primary receiver
            {
                if (primaryReceiver != null)
                {
                    return receivers[0];
                }
            }
            check = check - 2;
            if (check < 0)
                check = 0;
            return receivers[check];

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cc">ChainCrew</param>
        /// <param name="thrower">Player</param>
        private void OnInterceptionOccurred(ChainCrew cc, Player thrower)
        { 
           
            Interception interception = new Interception(cc, thrower, cc.TeamNotInPossession);
            int length= interception.Execute();
            length=cc.MoveBallDefensiveReturn(interception);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int Execute()
        {
            
            int pass = Dice.Roll("d100");
            bool isTd = false;
            int qbRating=thrower.PlayerSkills.Pass+this.defense.TeamDefense.PassRushRating;
            //check for auto td or interception
            if (pass == 1)
            {
                //auto td
                playLength = cc.GetAutoTDDistance();
                if (playLength == 0)
                    Console.WriteLine();
                playLength = cc.MoveBallPass(this, false, false, false);
                if (playLength == 0)
                    Console.WriteLine();
                isTd = true;
                gamePassStats.CompletePass(thrower, principalBallcarrier, playLength, true);
                gameReceiverStats.AddRun(playLength, isTd, false);
                ((PassPlayStatSheet)this.thrower.Stats[StatTypes.Pass]).CompletePass(thrower, principalBallcarrier, playLength, true);
                ((CarryStatSheet)this.principalBallcarrier.Stats[StatTypes.Receive]).AddRun(playLength, isTd, false);
                
            }
            else if (pass == 100)
            {
                //auto interception
                //go to interception module
                OnInterceptionOccurred(cc, thrower);
            }
            else if (pass <= qbRating)//thrower pass rating here
            {
                playLength=Dice.Roll(principalBallcarrier.PlayerSkills.Receive);
                if (playLength == 0)
                    Console.WriteLine();
                playLength = cc.MoveBallPass(this, false, false, false);
                if (playLength == 0)
                    Console.WriteLine();
                gamePassStats.CompletePass(thrower, principalBallcarrier, playLength, false);
                gameReceiverStats.AddRun(playLength, false, false);
                ((PassPlayStatSheet)this.thrower.Stats[StatTypes.Pass]).CompletePass(thrower, principalBallcarrier, playLength, false);
                ((CarryStatSheet)this.principalBallcarrier.Stats[StatTypes.Receive]).AddRun(playLength, false, false);  
            }
            else
            {
                //incomplete pass
                PassPlayResult res = this.defense.TeamDefense.PassDefense(Dice.Roll("d12"));
                gamePassStats.IncompletePass(thrower, res);
                ((PassPlayStatSheet)this.thrower.Stats[StatTypes.Pass]).IncompletePass(thrower,res);
                switch (res)
                {

                    case PassPlayResult.Sack:
                        string rollString = String.Format("d{0}", defense.TeamDefense.MaxSackLoss);
                        bool isFumble=CheckFumble(thrower);
                        playLength = -(Dice.Roll(rollString));
                        playLength = cc.MoveBallRun(this, isFumble);
                        gameRunStats.AddRun(playLength, false, isFumble);
                        ((CarryStatSheet)this.thrower.Stats[StatTypes.Run]).AddRun(playLength,false,isFumble);
                        
                        break;
                    case PassPlayResult.Incomplete:
                        playLength = 0;
                        cc.MoveBallPass(this,false,false,false);
                        break;
                    case PassPlayResult.Interception:
                        OnInterceptionOccurred(cc, thrower);
                        break;
                }
            }
            return playLength;
        }

        /// <summary>
        /// 
        /// </summary>
        public Player Thrower
        {
            get { return thrower; }
        }
    }
}
