namespace JSON
{
    internal class Parser
    {
        public static JSON parse(string json)
        {
            json = json.Trim();

            Dictionary<string, JSON> props = new Dictionary<string, JSON>();
            Types type = new Types();
            JSON  res  = new JSON(); 

            // reading object
            if (json[0] == '{')
            {
                res.type = Types.Object;
            }
            // reading array
            else if (json[0] == '[')
            {
                res.type = Types.Array;
            }
            // it's a value
            else
            {
                if (json == "true" || json == "false")
                {
                    res.type = Types.Bool;
                    res.bValue = json == "true";
                }
                else if (json == "null")
                {
                    res.type = Types.Null;
                }
                else if (json[0] == '"' && json[json.Length-1] == '"')
                {
                    res.type = Types.String;
                    res.strValue = json.Substring(1, json.Length - 2);
                }
                // number
                else 
                {
                    try
                    {
                        res.type = Types.Number;
                        if (json.Contains('.'))
                        {
                            res.dValue = Double.Parse(json);
                        }
                        else
                        {
                            res.nValue = long.Parse(json);
                        }
                    } catch
                    {
                        throw new Exception("Invalid number @JSON.parse");
                    }
                }
            }

            return new JSON(Types.Object, "test");
        }
    }
    public enum Types { Object /* {,} */, Array /* [,] */, Number /* [0-9]* */, String /* "" */, Null /* null */, Bool /* true|false */ }
    public class JSON
    {
        public string name { get; private set; }
        public Types  type;

        public string   strValue;
        public double   dValue;
        public long     nValue;
        public bool     bValue;

        long length;

        public Dictionary<string, JSON> props = new Dictionary<string, JSON>();
        public JSON(Types type, string name)
        {
            this.type = type;
            this.name = name;
        }
        public JSON() { }
    }
}
