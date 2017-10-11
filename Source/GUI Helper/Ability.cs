using System.Collections.Generic;
using System.Drawing;

namespace Wc3Engine
{
    public class Ability
    {
        public static TLVBase TLVCustom;
        public static TLVBase TLVStandard;
        public static List<Ability> MasterList = new List<Ability>();

        public string Name;
        public string Suffix;
        public int Id;
        public string CodeId;
        public string[] Tooltip;
        public Image Icon;
        public Image DisabledIcon;
        public AbilityModel Model;
        public Missile Missile;

        private AbilityStruct Struct;

        public Ability(string modelPath, string name, string suffix, int id)
        {
            Struct = new AbilityStruct(name, suffix);

            Name = name;
            Suffix = suffix;

            if (0 == id)
            {
                Id = ObjectId.GenerateAbilityId();
                Model = new AbilityModel(TLVCustom, modelPath, null, Id);
            }
            else
            {
                Id = id;
                ObjectId.AddAbilityId(id);
                Model = new AbilityModel(TLVStandard, modelPath, null, Id);
            }

            CodeId = Id.ToObjectId();

            //if (!AbilityMasterList.Exists(x => x.Path == itemPath))
               // Model = new AbilityModel(this, tlvAbilityPath, null, 0) != null;

            //Id = id;
            
            //Model = item;

            Missile = new Missile(null);

            MasterList.Add(this);
        }

        public static bool Create(string modelPath, string name)
        {
            return null != new Ability(modelPath, name, "", 0);
        }

        public static bool Create(string modelPath, string name, string suffix)
        {
            return null != new Ability(modelPath, name, suffix, 0);
        }

        public static bool Create(string modelPath, string name, int id)
        {
            return null != new Ability(modelPath, name, "", id);
        }

        public static bool Create(string modelPath, string name, string suffix, int id)
        {
            return null != new Ability(modelPath, name, suffix, id);
        }

        public void UpdateOnSelect()
        {
            Missile.UpdateOnSelect();
        }

        public static bool Exist(int id)
        {
            return MasterList.Exists(x => x.Id == id);
        }

        public static bool Exist(string codeId)
        {
            return MasterList.Exists(x => x.CodeId == codeId);
        }

        public static Ability Find(int id)
        {
            return MasterList.Find(x => x.Id == id);
        }

        public static Ability Find(string codeId)
        {
            return MasterList.Find(x => x.CodeId == codeId);
        }
    }
}
