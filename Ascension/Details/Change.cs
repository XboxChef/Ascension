namespace Ascension.Details
{
    using System;

    public class Change
    {
        private Ascension.Details.ChangeType _changeType = Ascension.Details.ChangeType.None;
        private string _text = "";
        private float _version = 0f;

        public Change(float Version, Ascension.Details.ChangeType Type, string Text)
        {
            _version = Version;
            _changeType = Type;
            _text = Text;
        }

        public override string ToString()
        {
            string str = "";
            switch (_changeType)
            {
                case Ascension.Details.ChangeType.Add:
                    str = str + "+";
                    break;

                case Ascension.Details.ChangeType.Remove:
                    str = str + "-";
                    break;

                case Ascension.Details.ChangeType.Fix:
                    str = str + "*";
                    break;

                case Ascension.Details.ChangeType.Update:
                    str = str + "*";
                    break;
            }
            return (str + " " + Text);
        }

        public Ascension.Details.ChangeType ChangeType =>
            _changeType;

        public string Text =>
            _text;

        public float Version =>
            _version;
    }
}

