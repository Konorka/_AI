using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.IO;
using System.Reflection;
using System.Collections;

namespace SpaceBattle.Common
{
    static class Operations
    {
        public static int MyParse(string s)
        {
            int i;
            if (int.TryParse(s, out i))
            {
                return i;
            }
            else
            {
                return 0;
            }
        }


        public static string Var_dump(object obj, int recursion)
        {
            StringBuilder result = new StringBuilder();

            // Protect the method against endless recursion
            if (recursion < 5)
            {
                // Determine object type
                Type t = obj.GetType();

                // Get array with properties for this object
                PropertyInfo[] properties = t.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    try
                    {
                        // Get the property value
                        object value = property.GetValue(obj, null);

                        // Create indenting string to put in front of properties of a deeper level
                        // We'll need this when we display the property name and value
                        string indent = String.Empty;
                        string spaces = "|   ";
                        string trail = "|...";

                        if (recursion > 0)
                        {
                            indent = new StringBuilder(trail).Insert(0, spaces, recursion - 1).ToString();
                        }

                        if (value != null)
                        {
                            // If the value is a string, add quotation marks
                            string displayValue = value.ToString();
                            if (value is string) displayValue = String.Concat('"', displayValue, '"');

                            // Add property name and value to return string
                            result.AppendFormat("{0}{1} = {2};", indent, property.Name, displayValue);

                            try
                            {
                                if (!(value is ICollection))
                                {
                                    // Call var_dump() again to list child properties
                                    // This throws an exception if the current property value
                                    // is of an unsupported type (eg. it has not properties)
                                    result.Append(Var_dump(value, recursion + 1));
                                }
                                else
                                {
                                    // 2009-07-29: added support for collections
                                    // The value is a collection (eg. it's an arraylist or generic list)
                                    // so loop through its elements and dump their properties
                                    int elementCount = 0;
                                    foreach (object element in ((ICollection)value))
                                    {
                                        string elementName = String.Format("{0}[{1}]", property.Name, elementCount);
                                        indent = new StringBuilder(trail).Insert(0, spaces, recursion).ToString();

                                        // Display the collection element name and type
                                        result.AppendFormat("{0}{1} = {2};", indent, elementName, element.ToString());

                                        // Display the child properties
                                        result.Append(Var_dump(element, recursion + 2));
                                        elementCount++;
                                    }

                                    result.Append(Var_dump(value, recursion + 1));
                                }
                            }
                            catch { }
                        }
                        else
                        {
                            // Add empty (null) property to return string
                            result.AppendFormat("{0}{1} = {2}\n", indent, property.Name, "null");
                        }
                    }
                    catch
                    {
                        // Some properties will throw an exception on property.GetValue()
                        // I don't know exactly why this happens, so for now i will ignore them...
                    }
                }
            }
            return result.ToString();
        }

        public static IniFile IniFile = new IniFile(Environment.CurrentDirectory+"\\SpaceBattle.ini");


        public static string ReadMyString(this BinaryReader BR)
        {
            try
            {
                int len = BR.ReadInt32();
                byte[] buf = new byte[len];
                buf = BR.ReadBytes(len);
                return Encoding.UTF8.GetString(buf);
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static bool WriteMyString(this BinaryWriter BW, string s)
        {
            try
            {
                byte[] buf = Encoding.UTF8.GetBytes(s);
                BW.Write(buf.Length);
                BW.Write(buf);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool IsConnected(Socket sock)
        {
            try
            {
                if (sock.Connected)
                {
                    if (sock.Poll(0, SelectMode.SelectWrite) && !sock.Poll(0, SelectMode.SelectError))
                    {
                        byte[] buffer = new byte[1];
                        if (sock.Receive(buffer, SocketFlags.Peek) == 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
