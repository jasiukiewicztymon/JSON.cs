/* JSON library
 * License: MIT
 * Creator: github@jasiukiewicztymon
 */

namespace JSON
{
    internal class Parser
    {
        public static JSON parse(string json)
        {
            json = json.Trim();

            Dictionary<string, JSON> props = new Dictionary<string, JSON>();
            JSON  res  = new JSON(); 

            // reading object
            if (json[0] == '{')
            {
                if (json[json.Length - 1] != '}')
                    throw new Exception("Object is not closed @JSON.parse");
                res.type = Types.Object;

                var clearjson = json.Substring(1, json.Length - 2);

                int a = 0, o = 0, l = 0;
                bool s = false;
                for (int i = 0; i < clearjson.Length; i++)
                {
                    if (clearjson[i] == '[')
                        a++;
                    else if (clearjson[i] == ']')
                        a--;
                    else if (clearjson[i] == '{')
                        o++;
                    else if (clearjson[i] == '}')
                        o--;

                    if (clearjson[i] == '"')
                    {
                        int b = 0;
                        while (clearjson[i - b - 1] == '\\')
                        {
                            b++;
                        }

                        if (b % 2 == 0)
                        {
                            if (s)
                            {
                                s = false;
                            }
                            else
                            {
                                s = true;
                            }
                        }
                    }

                    if (s && a == 0 && o == 0 && clearjson[i] == ',')
                    {

                    }
                }

                foreach (var element in elements)
                {
                    var hashmap = element.Split(':');
                    if (hashmap.Length != 2)
                        throw new Exception("Wrong object key, value assign @JSON.parse");

                    hashmap[0] = hashmap[0].Trim();
                    hashmap[1] = hashmap[1].Trim();

                    if (hashmap[0][0] == '"' && hashmap[0][hashmap[0].Length - 1] == '"')
                        props.Add(hashmap[0].Substring(1, hashmap[0].Length - 1), parse(hashmap[1]));
                    else
                        throw new Exception("Invalid object key @JSON.parse");
                }
                res.props = props;
            }
            // reading array
            else if (json[0] == '[')
            {
                if (json[json.Length - 1] != ']')
                    throw new Exception("Array is not closed @JSON.parse");
                res.type = Types.Array;

                var elements = json.Substring(1, json.Length - 2).Split(','); 

                for (int i = 0; i < elements.Length; i++)
                    props.Add(i.ToString(), parse(elements[i]));
                res.props = props;
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
                else if (json[0] == '"')
                {
                    if (json[json.Length - 1] != '"')
                        throw new Exception("Invalid string @JSON.parse");
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

            return res;
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

        public Dictionary<string, JSON> props = new Dictionary<string, JSON>();
        public JSON(Types type, string name)
        {
            this.type = type;
            this.name = name;
        }
        public JSON() { }

        public (bool Null, Types Type, string String, double Double, long Long, bool Bool) get()
        {
            if (type == Types.Object || type == Types.Array)
                throw new Exception($"Impossible to get from {type}, a index is needed @JSON.get");
            return (Types.Null == type, type, strValue, dValue, nValue, bValue);
        }
        public JSON get(string key)
        {
            if (type != Types.Object)
                throw new Exception($"Impossible to get with key from {type} @JSON.get");
            return props[key];
        }
        public JSON get(int index)
        {
            if (type != Types.Array)
                throw new Exception($"Impossible to get with index from {type} @JSON.get");
            return props[index.ToString()];
        }
    }
}
