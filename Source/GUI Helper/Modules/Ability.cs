using System.Collections.Generic;
using System.Drawing;

namespace Wc3Engine
{
    public class Ability
    {
        public static List<Ability> MasterList = new List<Ability>();

        public string Name;
        public string Suffix;
        public int Id;
        public string CodeId;
        public string[] Tooltip;
        public Image Icon;
        public Image DisabledIcon;
        public TLVModel.Item ItemModel;
        public Missile Missile;

        private AbilityStruct Struct;

        public Ability(string name, string suffix, int id, TLVModel.Item item)
        {
            Struct = new AbilityStruct(name, suffix);

            Name = name;
            Suffix = suffix;
            Id = id;
            CodeId = Id.ToObjectId();
            ItemModel = item;

            Missile = new Missile(null);

            MasterList.Add(this);
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
