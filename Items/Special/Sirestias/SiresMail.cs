﻿using LunarVeilLegacy.Helpers;
using LunarVeilLegacy.UI.Dialogue;
using LunarVeilLegacy.UI.Scripture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using LunarVeilLegacy.Assets.Biomes;
using LunarVeilLegacy.Dusts;
using LunarVeilLegacy.Items.Accessories;
using LunarVeilLegacy.Items.Accessories.Brooches;
using LunarVeilLegacy.Items.Armors.Vanity.Gia;
using LunarVeilLegacy.Items.Consumables;
using LunarVeilLegacy.Items.Harvesting;
using LunarVeilLegacy.Items.Materials;
using LunarVeilLegacy.Items.Materials.Tech;
using LunarVeilLegacy.Items.Ores;
using LunarVeilLegacy.Items.Placeable;
using LunarVeilLegacy.Items.Quest.BORDOC;
using LunarVeilLegacy.Items.Quest.Merena;
using LunarVeilLegacy.Items.Weapons.Igniters;
using LunarVeilLegacy.Items.Weapons.Mage;
using LunarVeilLegacy.Items.Weapons.Mage.Stein;
using LunarVeilLegacy.Items.Weapons.Melee;
using LunarVeilLegacy.Items.Weapons.Melee.Greatswords;
using LunarVeilLegacy.Items.Weapons.Melee.Safunais;
using LunarVeilLegacy.Items.Weapons.PowdersItem;
using LunarVeilLegacy.Items.Weapons.Ranged;
using LunarVeilLegacy.Items.Weapons.Summon;
using LunarVeilLegacy.Items.Weapons.Thrown;
using LunarVeilLegacy.Items.Weapons.Whips;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.Localization;
using Terraria.ModLoader.Utilities;
using Terraria.Utilities;

namespace LunarVeilLegacy.Items.Special.Sirestias
{
    internal class SiresMail : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.rare = ModContent.RarityType<SirestiasSpecialRarity>();
        }


        public override bool? UseItem(Player player)
        {
            DialogueSystem dialogueSystem = ModContent.GetInstance<DialogueSystem>();
            if (!DownedBossSystem.downedGintzlBoss)
            {
                switch (Main.rand.Next(3))
                {


                    case 0:
                            CallDialogue1 exampleDialogue = new CallDialogue1();

                       
                             dialogueSystem.StartDialogue(exampleDialogue);
                        break;

                    case 1:
                        CallDialogue1 exampleDialogue2 = new CallDialogue1();


                        dialogueSystem.StartDialogue(exampleDialogue2);
                        break;


                    case 2:
                        CallDialogue1 exampleDialogue3 = new CallDialogue1();


                        dialogueSystem.StartDialogue(exampleDialogue3);
                        break;



                }
                      



            }

            if (DownedBossSystem.downedGintzlBoss)
            {

                if (player.GetModPlayer<MyPlayer>().ZoneFable)
                switch (Main.rand.Next(1))
                {


                    case 0:
                        CallDialogue2 exampleDialogue = new CallDialogue2();


                        dialogueSystem.StartDialogue(exampleDialogue);
                        break;

                   



                }

                if (!player.GetModPlayer<MyPlayer>().ZoneFable)
                    switch (Main.rand.Next(1))
                    {


                        case 0:
                            CallDialogue3 exampleDialogue = new CallDialogue3();


                            dialogueSystem.StartDialogue(exampleDialogue);
                            break;





                    }



                if (DownedBossSystem.downedDaedusBoss)
                {



                    if (!player.GetModPlayer<MyPlayer>().ZoneAcid)
                        switch (Main.rand.Next(2))
                        {


                            case 0:
                                CallDialogue4 exampleDialogue = new CallDialogue4();


                                dialogueSystem.StartDialogue(exampleDialogue);
                                break;



                            case 1:
                                CallDialogue5 exampleDialogue2 = new CallDialogue5();


                                dialogueSystem.StartDialogue(exampleDialogue2);
                                break;

                        }



                    if (player.GetModPlayer<MyPlayer>().ZoneAcid)
                        switch (Main.rand.Next(1))
                        {


                            case 0:
                                CallDialogue6 exampleDialogue = new CallDialogue6();


                                dialogueSystem.StartDialogue(exampleDialogue);
                                break;



                        }








                    if (NPC.downedBoss2)
                    {

                        if (!player.GetModPlayer<MyPlayer>().ZoneCinder)
                        {
                            switch (Main.rand.Next(1))
                            {


                                case 0:
                                    CallDialogue7 exampleDialogue = new CallDialogue7();


                                    dialogueSystem.StartDialogue(exampleDialogue);
                                    break;



                            }


                        }






                        if (player.GetModPlayer<MyPlayer>().ZoneCinder)
                        {
                            switch (Main.rand.Next(1))
                            {


                                case 0:
                                    CallDialogue8 exampleDialogue = new CallDialogue8();


                                    dialogueSystem.StartDialogue(exampleDialogue);
                                    break;



                            }


                        }


                        if (DownedBossSystem.downedSOMBoss)
                        {

                            switch (Main.rand.Next(1))
                            {


                                case 0:
                                    CallDialogue9 exampleDialogue = new CallDialogue9();


                                    dialogueSystem.StartDialogue(exampleDialogue);
                                    break;



                            }







                            if (DownedBossSystem.downedVeriBoss)
                            {

                                switch (Main.rand.Next(2))
                                {


                                    case 0:
                                        CallDialogue10 exampleDialogue = new CallDialogue10();


                                        dialogueSystem.StartDialogue(exampleDialogue);
                                        break;


                                    case 1:
                                        CallDialogue11 exampleDialogue2 = new CallDialogue11();


                                        dialogueSystem.StartDialogue(exampleDialogue2);
                                        break;
                                }






                                if (Main.hardMode)
                                {

                                    switch (Main.rand.Next(1))
                                    {


                                        case 0:
                                            CallDialogue12 exampleDialogue = new CallDialogue12();


                                            dialogueSystem.StartDialogue(exampleDialogue);
                                            break;


                                 
                                    }








                                    if (DownedBossSystem.downedZuiBoss)
                                    {

                                        switch (Main.rand.Next(1))
                                        {


                                            case 0:
                                                CallDialogue13 exampleDialogue = new CallDialogue13();


                                                dialogueSystem.StartDialogue(exampleDialogue);
                                                break;



                                        }



                                        if (DownedBossSystem.downedSupernovaFragmentBoss)
                                        {

                                            switch (Main.rand.Next(1))
                                            {


                                                case 0:
                                                    CallDialogue14 exampleDialogue = new CallDialogue14();


                                                    dialogueSystem.StartDialogue(exampleDialogue);
                                                    break;



                                            }



                                            if (NPC.downedMoonlord)
                                            {

                                                switch (Main.rand.Next(1))
                                                {


                                                    case 0:
                                                        CallDialogue15 exampleDialogue = new CallDialogue15();


                                                        dialogueSystem.StartDialogue(exampleDialogue);
                                                        break;



                                                }


                                                if (DownedBossSystem.downedRekBoss)
                                                {

                                                    switch (Main.rand.Next(1))
                                                    {


                                                        case 0:
                                                            CallDialogue16 exampleDialogue = new CallDialogue16();


                                                            dialogueSystem.StartDialogue(exampleDialogue);
                                                            break;



                                                    }


                                                    if (DownedBossSystem.downedGothBoss)
                                                    {

                                                        switch (Main.rand.Next(1))
                                                        {


                                                            case 0:
                                                                CallDialogue17 exampleDialogue = new CallDialogue17();


                                                                dialogueSystem.StartDialogue(exampleDialogue);
                                                                break;



                                                        }





                                                    }


                                                }


                                            }


                                        }

                                    }















                                    }






                                }




                        }




                    }


                }






















            }
                //1. Get the dialogue system


                //2. Create a new instance of your dialogue

                return true;
        }
    }








        internal class CallDialogue9 : Dialogue
        {
            //The number of steps in this dialogue
            public override int Length => 1;

            public override void Next(int index)
            {
                base.Next(index);

                //This starts the dialogue
                switch (index)
                {
                    case 0:
                        //Set the texture of the portrait
                        DialogueSystem.SetPortrait("LunarVeilLegacy/UI/Dialogue/SirestiasDialoguePortrait");

                        //Put your dialogue in Mods.LunarVeilLegacy.Dialogue.hjson, then get it like this
                        DialogueSystem.WriteText(GetLocalizedText("SirestiasTalk15"));
                        break;



                }
            }

            public override void Update(int index)
            {
                base.Update(index);
                //If you want stuff to happen while they're talking you can do it here ig
                //But that might not be a good idea since you can just speed through dialogues
            }

            public override void Complete()
            {

                //Do something when the dialogue is completely finished


                base.Complete();
            }




        
    }



    //---------------------------






    internal class CallDialogue10 : Dialogue
    {
        //The number of steps in this dialogue
        public override int Length => 3;

        public override void Next(int index)
        {
            base.Next(index);

            //This starts the dialogue
            switch (index)
            {
                case 0:
                    //Set the texture of the portrait
                    DialogueSystem.SetPortrait("LunarVeilLegacy/UI/Dialogue/SirestiasDialoguePortrait");

                    //Put your dialogue in Mods.LunarVeilLegacy.Dialogue.hjson, then get it like this
                    DialogueSystem.WriteText(GetLocalizedText("SirestiasTalk16"));
                    break;


                case 1:
                    //Set the texture of the portrait
           

                    //Put your dialogue in Mods.LunarVeilLegacy.Dialogue.hjson, then get it like this
                    DialogueSystem.WriteText(GetLocalizedText("SirestiasTalk17"));
                    break;


                case 2:
                    //Set the texture of the portrait
                  

                    //Put your dialogue in Mods.LunarVeilLegacy.Dialogue.hjson, then get it like this
                    DialogueSystem.WriteText(GetLocalizedText("SirestiasTalk18"));
                    break;


            }
        }

        public override void Update(int index)
        {
            base.Update(index);
            //If you want stuff to happen while they're talking you can do it here ig
            //But that might not be a good idea since you can just speed through dialogues
        }

        public override void Complete()
        {

            //Do something when the dialogue is completely finished


            base.Complete();
        }





    }












    internal class CallDialogue11 : Dialogue
    {
        //The number of steps in this dialogue
        public override int Length => 3;

        public override void Next(int index)
        {
            base.Next(index);

            //This starts the dialogue
            switch (index)
            {
                case 0:
                    //Set the texture of the portrait
                    DialogueSystem.SetPortrait("LunarVeilLegacy/UI/Dialogue/SirestiasDialoguePortrait");

                    //Put your dialogue in Mods.LunarVeilLegacy.Dialogue.hjson, then get it like this
                    DialogueSystem.WriteText(GetLocalizedText("SirestiasTalk19"));
                    break;


                case 1:
                    //Set the texture of the portrait


                    //Put your dialogue in Mods.LunarVeilLegacy.Dialogue.hjson, then get it like this
                    DialogueSystem.WriteText(GetLocalizedText("SirestiasTalk17"));
                    break;


                case 2:
                    //Set the texture of the portrait


                    //Put your dialogue in Mods.LunarVeilLegacy.Dialogue.hjson, then get it like this
                    DialogueSystem.WriteText(GetLocalizedText("SirestiasTalk18"));
                    break;


            }
        }

        public override void Update(int index)
        {
            base.Update(index);
            //If you want stuff to happen while they're talking you can do it here ig
            //But that might not be a good idea since you can just speed through dialogues
        }

        public override void Complete()
        {

            //Do something when the dialogue is completely finished


            base.Complete();
        }





    }













    internal class CallDialogue12 : Dialogue
    {
        //The number of steps in this dialogue
        public override int Length => 1;

        public override void Next(int index)
        {
            base.Next(index);

            //This starts the dialogue
            switch (index)
            {
                case 0:
                    //Set the texture of the portrait
                   // DialogueSystem.SetPortrait("LunarVeilLegacy/UI/Dialogue/SirestiasDialoguePortrait");

                    //Put your dialogue in Mods.LunarVeilLegacy.Dialogue.hjson, then get it like this
                    DialogueSystem.WriteText(GetLocalizedText("SirestiasTalk20"));
                    break;


                

            }
        }

        public override void Update(int index)
        {
            base.Update(index);
            //If you want stuff to happen while they're talking you can do it here ig
            //But that might not be a good idea since you can just speed through dialogues
        }

        public override void Complete()
        {

            //Do something when the dialogue is completely finished


            base.Complete();
        }





    }




    internal class CallDialogue13 : Dialogue
    {
        //The number of steps in this dialogue
        public override int Length => 2;

        public override void Next(int index)
        {
            base.Next(index);

            //This starts the dialogue
            switch (index)
            {
                case 0:
                    //Set the texture of the portrait
                     DialogueSystem.SetPortrait("LunarVeilLegacy/UI/Dialogue/SirestiasDialoguePortrait");

                    //Put your dialogue in Mods.LunarVeilLegacy.Dialogue.hjson, then get it like this
                    DialogueSystem.WriteText(GetLocalizedText("SirestiasTalk21"));
                    break;

                case 1:
                    //Set the texture of the portrait
                    DialogueSystem.SetPortrait("LunarVeilLegacy/UI/Dialogue/SirestiasDialoguePortrait");

                    //Put your dialogue in Mods.LunarVeilLegacy.Dialogue.hjson, then get it like this
                    DialogueSystem.WriteText(GetLocalizedText("SirestiasTalk22"));
                    break;


            }
        }

        public override void Update(int index)
        {
            base.Update(index);
            //If you want stuff to happen while they're talking you can do it here ig
            //But that might not be a good idea since you can just speed through dialogues
        }

        public override void Complete()
        {

            //Do something when the dialogue is completely finished


            base.Complete();
        }





    }

    internal class CallDialogue14 : Dialogue
    {
        //The number of steps in this dialogue
        public override int Length => 3;

        public override void Next(int index)
        {
            base.Next(index);

            //This starts the dialogue
            switch (index)
            {
                case 0:
                    //Set the texture of the portrait
                    DialogueSystem.SetPortrait("LunarVeilLegacy/UI/Dialogue/SirestiasDialoguePortrait");

                    //Put your dialogue in Mods.LunarVeilLegacy.Dialogue.hjson, then get it like this
                    DialogueSystem.WriteText(GetLocalizedText("SirestiasTalk23"));
                    break;

                case 1:
                    //Set the texture of the portrait
                    DialogueSystem.SetPortrait("LunarVeilLegacy/UI/Dialogue/SirestiasDialoguePortrait");

                    //Put your dialogue in Mods.LunarVeilLegacy.Dialogue.hjson, then get it like this
                    DialogueSystem.WriteText(GetLocalizedText("SirestiasTalk24"));
                    break;

                case 2:
                    //Set the texture of the portrait
                    DialogueSystem.SetPortrait("LunarVeilLegacy/UI/Dialogue/SirestiasDialoguePortrait");

                    //Put your dialogue in Mods.LunarVeilLegacy.Dialogue.hjson, then get it like this
                    DialogueSystem.WriteText(GetLocalizedText("SirestiasTalk25"));
                    break;
            }
        }

        public override void Update(int index)
        {
            base.Update(index);
            //If you want stuff to happen while they're talking you can do it here ig
            //But that might not be a good idea since you can just speed through dialogues
        }

        public override void Complete()
        {

            //Do something when the dialogue is completely finished


            base.Complete();
        }





    }






    internal class CallDialogue15 : Dialogue
    {
        //The number of steps in this dialogue
        public override int Length => 3;

        public override void Next(int index)
        {
            base.Next(index);

            //This starts the dialogue
            switch (index)
            {
                case 0:
                    //Set the texture of the portrait
                    DialogueSystem.SetPortrait("LunarVeilLegacy/UI/Dialogue/SirestiasDialoguePortrait");

                    //Put your dialogue in Mods.LunarVeilLegacy.Dialogue.hjson, then get it like this
                    DialogueSystem.WriteText(GetLocalizedText("SirestiasTalk26"));
                    break;

                case 1:
                    //Set the texture of the portrait
                    DialogueSystem.SetPortrait("LunarVeilLegacy/UI/Dialogue/SirestiasDialoguePortrait");

                    //Put your dialogue in Mods.LunarVeilLegacy.Dialogue.hjson, then get it like this
                    DialogueSystem.WriteText(GetLocalizedText("SirestiasTalk27"));
                    break;

                case 2:
                    //Set the texture of the portrait
                    DialogueSystem.SetPortrait("LunarVeilLegacy/UI/Dialogue/SirestiasDialoguePortrait");

                    //Put your dialogue in Mods.LunarVeilLegacy.Dialogue.hjson, then get it like this
                    DialogueSystem.WriteText(GetLocalizedText("SirestiasTalk28"));
                    break;
            }
        }

        public override void Update(int index)
        {
            base.Update(index);
            //If you want stuff to happen while they're talking you can do it here ig
            //But that might not be a good idea since you can just speed through dialogues
        }

        public override void Complete()
        {

            //Do something when the dialogue is completely finished


            base.Complete();
        }





    }



    internal class CallDialogue16 : Dialogue
    {
        //The number of steps in this dialogue
        public override int Length => 1;

        public override void Next(int index)
        {
            base.Next(index);

            //This starts the dialogue
            switch (index)
            {
                case 0:
                    //Set the texture of the portrait
                    DialogueSystem.SetPortrait("LunarVeilLegacy/UI/Dialogue/SirestiasDialoguePortrait");

                    //Put your dialogue in Mods.LunarVeilLegacy.Dialogue.hjson, then get it like this
                    DialogueSystem.WriteText(GetLocalizedText("SirestiasTalk29"));
                    break;


            }
        }

        public override void Update(int index)
        {
            base.Update(index);
            //If you want stuff to happen while they're talking you can do it here ig
            //But that might not be a good idea since you can just speed through dialogues
        }

        public override void Complete()
        {

            //Do something when the dialogue is completely finished


            base.Complete();
        }





    }


    internal class CallDialogue17 : Dialogue
    {
        //The number of steps in this dialogue
        public override int Length => 1;

        public override void Next(int index)
        {
            base.Next(index);

            //This starts the dialogue
            switch (index)
            {
                case 0:
                    //Set the texture of the portrait
              //      DialogueSystem.SetPortrait("LunarVeilLegacy/UI/Dialogue/SirestiasDialoguePortrait");

                    //Put your dialogue in Mods.LunarVeilLegacy.Dialogue.hjson, then get it like this
                    DialogueSystem.WriteText(GetLocalizedText("SirestiasTalk30"));
                    break;


            }
        }

        public override void Update(int index)
        {
            base.Update(index);
            //If you want stuff to happen while they're talking you can do it here ig
            //But that might not be a good idea since you can just speed through dialogues
        }

        public override void Complete()
        {

            //Do something when the dialogue is completely finished


            base.Complete();
        }





    }








}
