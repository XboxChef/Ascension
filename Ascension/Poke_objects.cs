namespace Ascension
{
    using System;
    using System.Windows.Forms;

    public class Poke_objects
    {
        public Affects effects;
        public ToolStripMenuItem menu_placement;
        public string poke_altered;
        public string poke_initial;
        public string poke_name;
        public uint poke_offset;
        public string poke_type;

        public Poke_objects(ToolStripMenuItem menu_plc, string value_name)
        {
            effects = Affects.Default;
            menu_placement = menu_plc;
            poke_name = value_name;
        }

        public Poke_objects(ToolStripMenuItem menu_plc, string value_name, uint offsetx, string typex, string init, Affects effe)
        {
            effects = Affects.Default;
            menu_placement = menu_plc;
            poke_name = value_name;
            poke_offset = offsetx;
            poke_type = typex;
            poke_initial = init;
            effects = effe;
        }

        public Poke_objects(ToolStripMenuItem menu_plc, string value_name, uint offsetx, string typex, string init, string alte, Affects effe)
        {
            effects = Affects.Default;
            menu_placement = menu_plc;
            poke_name = value_name;
            poke_offset = offsetx;
            poke_type = typex;
            poke_initial = init;
            poke_altered = alte;
            effects = effe;
        }

        public enum Affects
        {
            Default = 3,
            Irreversable = 2,
            OnMapReload = 1
        }
    }
}

