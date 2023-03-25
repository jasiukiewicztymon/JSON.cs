/* JSON library
 * License: MIT
 * Creator: github@jasiukiewicztymon
 * v1.0
 */

namespace JSON
{
    internal class Parser
    {
        public static JSON parse(string json)
        {
            while (json[0] == ' ' || json[0] == '\t' || json[0] == '\n' || json[0] == '\r')
                json = json.Remove(0, 1);
            while (json[json.Length - 1] == ' ' || json[json.Length - 1] == '\t' || json[json.Length - 1] == '\n' || json[json.Length - 1] == '\r')
                json = json.Remove(json.Length - 1, 1);

            List<JSON> tree = new List<JSON>();

            int i = 0;
            while (i < json.Length)
            {
                if (tree.Count >= 1 && i < json.Length && json[i] == ',')
                {
                    i++;
                    while (i < json.Length && (json[i] == ' ' || json[i] == '\t' || json[i] == '\n' || json[i] == '\r'))
                        i++;
                    if (json[i] == '}' || json[i] == ']')
                        throw new Exception($"@JSON.parse : There is a seperation token but any value has been detected at {i}");
                }
                while (i < json.Length && (json[i] == ' ' || json[i] == '\t' || json[i] == '\n' || json[i] == '\r'))
                    i++;

                if (json[i] == '{')
                {
                    i++;
                    while (i < json.Length && (json[i] == ' ' || json[i] == '\t' || json[i] == '\n' || json[i] == '\r'))
                        i++;
                    if (json[i] != '"' && json[i] != '}')
                        throw new Exception($"@JSON.parse : Invalid token at {i} in json object key");

                    tree.Add(new JSON(Types.Object));
                }
                else if (json[i] == '[')
                {
                    i++;
                    while (i < json.Length && (json[i] == ' ' || json[i] == '\t' || json[i] == '\n' || json[i] == '\r'))
                        i++;

                    if (json[i] == ',')
                        throw new Exception($"@JSON.parse : There is a seperation token but any value has been detected at {i}");
                    tree.Add(new JSON(Types.Array));
                }
                else if (json[i] == '}' || json[i] == ']')
                {
                    if (tree.Count == 1)
                    {
                        if (i < json.Length - 1)
                            throw new Exception($"@JSON.parse : Invalid JSON at {i}");
                        return tree[0];
                    }
                        
                    if (tree.Count < 1)
                        throw new Exception($"@JSON.parse : Invalid close token at {i}");

                    if (tree[tree.Count - 2].type == Types.Object)
                    {
                        if ((tree[tree.Count - 1].type == Types.Object && json[i] == ']') || (tree[tree.Count - 1].type == Types.Array && json[i] == '}'))
                            throw new Exception($"@JSON.parse : Invalid close token at {i}");
                        if (tree[tree.Count - 2].name == "")
                            throw new Exception($"@JSON.parse : Missing object key at {i}");
                        tree[tree.Count - 2].props.Add(tree[tree.Count - 2].name, tree[tree.Count - 1]);
                        tree[tree.Count - 2].name = "";
                        tree.RemoveAt(tree.Count - 1);
                        i++;
                        while (i < json.Length && (json[i] == ' ' || json[i] == '\t' || json[i] == '\n' || json[i] == '\r'))
                            i++;
                        if (i < json.Length && json[i] != ',' && json[i] != '}')
                            throw new Exception($"@JSON.parse : Invalid token at {i}");
                    }
                    else
                    {
                        if ((tree[tree.Count - 1].type == Types.Object && json[i] == ']') || (tree[tree.Count - 1].type == Types.Array && json[i] == '}'))
                            throw new Exception($"@JSON.parse : Invalid close token at {i}");
                        tree[tree.Count - 2].props.Add(tree[tree.Count - 2].props.Count.ToString(), tree[tree.Count - 1]);
                        tree.RemoveAt(tree.Count - 1);
                        i++;
                        while (i < json.Length && (json[i] == ' ' || json[i] == '\t' || json[i] == '\n' || json[i] == '\r'))
                            i++;
                        if (i < json.Length && json[i] != ',' && json[i] != ']')
                            throw new Exception($"@JSON.parse : Invalid token at {i}");
                    }
                }
                else if (json[i] == '"')
                {
                    string value = "";
                    bool dis = false;

                    i++;

                    while (true)
                    {
                        if (json[i] == '"' && dis == false)
                            break;
                        if (json[i] == '\\')
                        {
                            if (dis)
                                value += json[i];
                            dis = !dis;
                        }
                        else
                            dis = false;

                        if (!dis || json[i] != '\\')
                            value += json[i];

                        i++;

                        if (i == json.Length)
                            throw new Exception("@JSON.parse : Infinite String");
                    }
                    i++;

                    if (tree.Count == 0)
                    {
                        if (i < json.Length - 1)
                            throw new Exception($"@JSON.parse : Invalid JSON at {i}");
                        return new JSON(value, Types.String);
                    }

                    if (tree[tree.Count - 1].type == Types.Object)
                    {
                        if (tree[tree.Count - 1].name == "")
                        {
                            if (value == "")
                                throw new Exception($"@JSON.parse : Invalid empty key at {i}");
                            tree[tree.Count - 1].name = value;

                            while (i < json.Length && (json[i] == ' ' || json[i] == '\t' || json[i] == '\n' || json[i] == '\r'))
                                i++;
                            if (i < json.Length && json[i] != ':')
                                throw new Exception($"@JSON.parse : Invalid token at {i} while json object assigning");

                            i++;
                            while (i < json.Length && (json[i] == ' ' || json[i] == '\t' || json[i] == '\n' || json[i] == '\r'))
                                i++;
                            if (i < json.Length && (json[i] == '}' || json[i] == ',' || json[i] == ']'))
                                throw new Exception($"@JSON.parse : Invalid object value at {i}");
                        }
                        else
                        {
                            tree[tree.Count - 1].props.Add(tree[tree.Count - 1].name, new JSON(value, Types.String));
                            tree[tree.Count - 1].name = "";
                            while (i < json.Length && (json[i] == ' ' || json[i] == '\t' || json[i] == '\n' || json[i] == '\r'))
                                i++;
                            if (i < json.Length && json[i] != ',' && json[i] != '}')
                                throw new Exception($"@JSON.parse : Invalid token at {i}");
                        }
                    }
                    else
                    {
                        tree[tree.Count - 1].props.Add(tree[tree.Count - 1].props.Count.ToString(), new JSON(value, Types.String));
                        while (i < json.Length && (json[i] == ' ' || json[i] == '\t' || json[i] == '\n' || json[i] == '\r'))
                            i++;
                        if (i < json.Length && json[i] != ',' && json[i] != ']')
                            throw new Exception($"@JSON.parse : Invalid token at {i}");
                    }
                }
                else
                {
                    string value = "";
                    while (i < json.Length && json[i] != ' ' && json[i] != '\t' && json[i] != '\n' && json[i] != '\r' && json[i] != '"' && json[i] != ']' && json[i] != '}' && json[i] != ',')
                    {
                        value += json[i];
                        i++;
                    }

                    if (value == "")
                        throw new Exception($"@JSON.parse : Invalid value at {i}");

                    while (i < json.Length && (json[i] == ' ' || json[i] == '\t' || json[i] == '\n' || json[i] == '\r'))
                        i++;

                    if (value == "null")
                    {
                        if (tree.Count == 0)
                        {
                            if (i < json.Length - 1)
                                throw new Exception($"@JSON.parse : Invalid JSON at {i}");
                            return new JSON(Types.Null);
                        }

                        if (tree[tree.Count - 1].type == Types.Object)
                        {
                            if (tree[tree.Count - 1].name == "")
                                throw new Exception($"@JSON.parse : Missing object key at {i}");
                            tree[tree.Count - 1].props.Add(tree[tree.Count - 1].name, new JSON(Types.Null));
                            tree[tree.Count - 1].name = "";
                            if (i < json.Length && json[i + 1] != ',' && json[i] != '}')
                                throw new Exception($"@JSON.parse : Invalid token at {i}");
                        }
                        else
                        {
                            tree[tree.Count - 1].props.Add(tree[tree.Count - 1].props.Count.ToString(), new JSON(Types.Null));
                            if (i < json.Length && json[i] != ',' && json[i] != ']')
                                throw new Exception($"@JSON.parse : Invalid token at {i}");
                        }
                    }
                    else if (value == "true" || value == "false")
                    {
                        if (tree.Count == 0)
                        {
                            if (i < json.Length - 1)
                                throw new Exception($"@JSON.parse : Invalid JSON at {i}");
                            return new JSON(value == "true", Types.Bool);
                        }

                        if (tree[tree.Count - 1].type == Types.Object)
                        {
                            if (tree[tree.Count - 1].name == "")
                                throw new Exception($"@JSON.parse : Missing object key at {i}");
                            tree[tree.Count - 1].props.Add(tree[tree.Count - 1].name, new JSON(value == "true", Types.Bool));
                            tree[tree.Count - 1].name = "";
                            if (i < json.Length && json[i] != ',' && json[i] != '}')
                                throw new Exception($"@JSON.parse : Invalid token at {i}");
                        }
                        else
                        {
                            tree[tree.Count - 1].props.Add(tree[tree.Count - 1].props.Count.ToString(), new JSON(value == "true", Types.Bool));
                            if (i < json.Length && json[i] != ',' && json[i] != ']')
                                throw new Exception($"@JSON.parse : Invalid token at {i}");
                        }
                    }
                    else
                    {
                        if (value.Contains("."))
                        {
                            double val = 0;
                            try
                            {
                                val = double.Parse(value.Replace('.', ','));
                            }
                            catch { throw new Exception($"@JSON.parse : Invalid value at {i}"); }

                            if (tree.Count == 0)
                            {
                                if (i < json.Length - 1)
                                    throw new Exception($"@JSON.parse : Invalid JSON at {i}");
                                return new JSON(val, Types.Number);
                            }

                            if (tree[tree.Count - 1].type == Types.Object)
                            {
                                if (tree[tree.Count - 1].name == "")
                                    throw new Exception($"@JSON.parse : Missing object key at {i}");
                                tree[tree.Count - 1].props.Add(tree[tree.Count - 1].name, new JSON(val, Types.Number));
                                tree[tree.Count - 1].name = "";
                                if (i < json.Length && json[i] != ',' && json[i] != '}')
                                    throw new Exception($"@JSON.parse : Invalid token at {i}");
                            }
                            else
                            {
                                tree[tree.Count - 1].props.Add(tree[tree.Count - 1].props.Count.ToString(), new JSON(val, Types.Number));
                                if (i < json.Length && json[i] != ',' && json[i] != ']')
                                    throw new Exception($"@JSON.parse : Invalid value at {i}");
                            }
                        }
                        else
                        {
                            long val = 0;
                            try
                            {
                                val = long.Parse(value);
                            }
                            catch { throw new Exception($"@JSON.parse : Invalid value at {i}"); }

                            if (tree.Count == 0)
                            {
                                if (i < json.Length - 1)
                                    throw new Exception($"@JSON.parse : Invalid JSON at {i}");
                                return new JSON(val, Types.Number);
                            }

                            if (tree[tree.Count - 1].type == Types.Object)
                            {
                                if (tree[tree.Count - 1].name == "")
                                    throw new Exception($"@JSON.parse : Missing object key at {i}");
                                tree[tree.Count - 1].props.Add(tree[tree.Count - 1].name, new JSON(val, Types.Number));
                                tree[tree.Count - 1].name = "";
                                if (i < json.Length && json[i] != ',' && json[i] != '}')
                                    throw new Exception($"@JSON.parse : Invalid token at {i}");
                            }
                            else
                            {
                                tree[tree.Count - 1].props.Add(tree[tree.Count - 1].props.Count.ToString(), new JSON(val, Types.Number));
                                if (i < json.Length && json[i] != ',' && json[i] != ']')
                                    throw new Exception($"@JSON.parse : Invalid token at {i}");
                            }
                        }
                    }
                    if (json[i] != ']' && json[i] !=  '}' && json[i] != ',')
                        i++;
                }
            }

            if (tree.Count != 1)
                throw new Exception("@JSON.parse : Invalid JSON");

            return tree[0];
        }
    }
    public enum Types { Object /* {,} */, Array /* [,] */, Number /* [0-9]* */, String /* "" */, Null /* null */, Bool /* true|false */ }
    public class JSON
    {
        public Types type;

        public string name = "";

        public string strValue;
        public double dValue;
        public long nValue;
        public bool bValue;

        public Dictionary<string, JSON> props = new Dictionary<string, JSON>();

        public JSON() { }
        public JSON(Types t) { type = t; }

        public JSON(string v, Types t) { type = t; strValue = v; }
        public JSON(double v, Types t) { type = t; dValue = v; }
        public JSON(long v, Types t) { type = t; nValue = v; }
        public JSON(bool v, Types t) { type = t; bValue = v; }

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
        public override string ToString()
        {
            if (type == Types.Array)
            {
                List<string> list = new List<string>();
                foreach (var item in props)
                {
                    list.Add(item.Value.ToString());
                }

                return "[" + String.Join(',', list) + "]";
            }
            else if (type == Types.Object)
            {
                List<string> list = new List<string>();
                foreach (var item in props)
                {
                    string t = "\"" + item.Key + "\":";
                    t += item.Value.ToString();
                    list.Add(t);
                }

                return "{" + String.Join(',', list) + "}";
            } 
            else if (type == Types.String)
            {
                return "\""+strValue+"\"";
            }
            else if (type == Types.Number)
            {
                if (nValue != 0)
                    return nValue.ToString();
                else if (dValue != 0)
                    return dValue.ToString();
                else
                    return "0";
            }
            else if (type == Types.Bool)
            {
                return bValue ? "true" : "false";
            }
            else
            {
                return "null";
            }
        }
    }
}
