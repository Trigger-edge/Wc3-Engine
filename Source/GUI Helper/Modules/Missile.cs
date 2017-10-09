using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wc3Engine
{
    public class Missile
    {
        private static AbilityStruct Struct;
        //public List<object> Basics;
        //public List<object> Effects;

        public int Count;
        public decimal Damage;
        public decimal Period;
        public decimal Duration;
        public decimal Arc;
        public decimal Speed;
        public decimal Collision;
        public decimal CastDist;
        public decimal Variation;

        public string Model;
        public string OnHit;
        public string OnDestroy;

        public Missile(AbilityStruct abilityStruct)
        {
            Struct = abilityStruct;

            /*Basics = new List<object>()
            {
                Count,
                Damage,
                Period,
                Duration,
                Arc,
                Speed,
                Collision,
                CastDist,
                Variation,
            };

            Effects = new List<object>()
            {
                Model,
                OnHit,
                OnDestroy,
            };*/
        }

        public void Update()
        {
            Count           = (int)Wc3Engine.This.GUI_Missile_count.Value;
            Damage          = Wc3Engine.This.GUI_Missile_damage.Value;
            Period          = Wc3Engine.This.GUI_Missile_period.Value;
            Duration        = Wc3Engine.This.GUI_Missile_duration.Value;
            Arc             = Wc3Engine.This.GUI_Missile_arc.Value;
            Speed           = Wc3Engine.This.GUI_Missile_speed.Value;
            Collision       = Wc3Engine.This.GUI_Missile_collision.Value;
            CastDist        = Wc3Engine.This.GUI_Missile_castdist.Value;
            Variation       = Wc3Engine.This.GUI_Missile_variation.Value;


            //if (!Struct.FunctionExist("OnCast"))
            //Struct.InsertFunction(Struct.Count, "OnCast", "nothing");

            /* if (0 < Wc3Engine.This.missileCount_numericUpDown.Value)
             {
                 //Jass.InsertFunctionToAbilityStruct("OnCast", abilityStructName, "Init", "nothing", "nothing");
                 //Jass.InsertCallToFunction(abilityStructName + "OnCast", "wtf??");
             }*/
        }

        public void UpdateOnSelect()
        {
            Wc3Engine.This.GUI_Missile_count.Value          = Count;
            Wc3Engine.This.GUI_Missile_damage.Value         = Damage;
            Wc3Engine.This.GUI_Missile_period.Value         = Period;
            Wc3Engine.This.GUI_Missile_duration.Value       = Duration;
            Wc3Engine.This.GUI_Missile_arc.Value            = Arc;
            Wc3Engine.This.GUI_Missile_speed.Value          = Speed;
            Wc3Engine.This.GUI_Missile_collision.Value      = Collision;
            Wc3Engine.This.GUI_Missile_castdist.Value       = CastDist;
            Wc3Engine.This.GUI_Missile_variation.Value      = Variation;
        }
    }
}
