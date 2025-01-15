using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CoordSystemApp
{
    public partial class Form1 : Form
    {
        static string[] wordlist = { "apple", "arm", "ant", "air", "ask", "all", "and", "after", "age", "act", "area", "animal", "alone", "always", "ball", "bat", "big", "boy", "bed", "bag", "bird", "blue", "book", "baby", "bread", "brown",
        "black", "bus", "cat", "car", "cup", "cake", "cold", "clean", "call", "come", "city", "child", "chair", "close", "class", "clear", "dog", "door", "desk", "day", "dark", "deep", "dress", "drop", "drink", "dance", "draw", "dish",
        "drive", "down", "egg", "ear", "eat", "end", "easy", "early", "earth", "enjoy", "enter", "edge", "every", "empty", "even", "exact", "fish", "fan", "fun", "food", "farm", "friend", "fast", "free", "face", "far", "fall", "family",
        "find", "fresh", "girl", "game", "green", "gold", "give", "good", "go", "glass", "grow", "great", "ground", "group", "glad", "guess", "hat", "hand", "home", "help", "hot", "hear", "house", "happy", "hard", "hope", "high", "half",
        "hold", "human", "ice", "idea", "iron", "inside", "item", "image", "invite", "island", "issue", "interest", "improve", "include", "invest", "ignore", "jump", "job", "join", "joke", "just", "joy", "jacket", "juice", "king", "key",
        "kite", "kangaroo", "kind", "keep", "kitchen", "kiss", "knife", "know", "kid", "kettle", "knock", "knee", "kingdom", "lion", "lake", "lamp", "leaf", "long", "laugh", "large", "light", "like", "library", "line", "learn", "love",
        "listen", "letter", "mouse", "man", "moon", "milk", "music", "money", "market", "minute", "middle", "morning", "mother", "mountain", "mouth", "mirror", "night", "name", "new", "near", "never", "next", "number", "nice", "nature",
        "noise", "north", "napkin", "neighbor", "note", "orange", "open", "over", "old", "only", "order", "office", "ocean", "offer", "outside", "onion", "often", "object", "owner", "paper", "pen", "paint", "phone", "plant", "place", "park",
        "party", "picture", "price", "piece", "point", "person", "queen", "question", "quiet", "quick", "quarter", "quit", "quality", "quote", "quilt", "quest", "quiz", "queue", "quack", "quill", "rabbit", "rain", "red", "run", "read", "river",
        "road", "room", "right", "roof", "rock", "reach", "round", "rule", "radio", "snake", "sun", "star", "sea", "ship", "small", "smile", "sleep", "school", "stone", "story", "stop", "start", "strong", "sugar", "table", "time", "tree",
        "tea", "train", "take", "turn", "talk", "touch", "teach", "try", "top", "taste", "uncle", "under", "use", "up", "until", "unit", "usual", "unique", "umbrella", "universe", "update", "unload", "useful", "unable", "voice", "visit",
        "very", "view", "vote", "village", "victory", "value", "velvet", "version", "vowel", "van", "violet", "whale", "water", "white", "window", "wood", "world", "watch", "wave", "wall", "winter", "walk", "welcome", "wonder", "wish", "woman",
        "x-ray", "yellow", "year", "young", "yes", "yard", "yardstick", "yarn", "yesterday", "yawn", "youth", "yield", "yogurt", "yourself", "zebra", "zoo", "zero", "zip", "zone", "zigzag", "zest", "zoom", "zipper", "zeal", "zany", "zap",
        "zestful", "zen", "zombie", "zinc", "zenith", "zapper", "zealot", "zoomed", "zodiac", "zigzagged", "zapped", "zealous", "zoology", "zookeeper", "zircon", "zeroed", "zoned" };


        static string EncodeNumber(int number)
        {
            if (number < -180 || number > 180)
            {
                throw new ArgumentOutOfRangeException("n", "Number must be between -180 and 180.");
            }

            number += 180;

            string word = wordlist[number];

            return word;
        }

        static int DecodeWord(string input_word)
        {
            if (!wordlist.Contains(input_word))
            {
                throw new ArgumentException("Word not found in the list.");
            }

            int index = Array.IndexOf(wordlist, input_word);
            return index - 180;  // Shift back to -180:180
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) // Encode button
        {
            if (!IsNumeric(textBox2.Text) || !IsNumeric(textBox3.Text) || !IsNumeric(textBox4.Text) || !IsNumeric(textBox5.Text))
            {
                label11.Text = "Error: invalid coordnates";
                return;
            }

            if (Convert.ToInt32(textBox2.Text) > 99 || Convert.ToInt32(textBox3.Text) > 99 || Convert.ToInt32(textBox4.Text) > 99 || Convert.ToInt32(textBox5.Text) > 99)
            {
                label11.Text = "Error: invalid coordnates";
                return;
            }

            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                label11.Text = "Error: can't have blank coordinates";
                return;
            }

            label11.Text = ""; // label11 is the error text label

            string lat_output_text = "";
            string long_output_text = "";


            lat_output_text += EncodeNumber(Convert.ToInt32(textBox1.Text));
            lat_output_text += " ";
            lat_output_text += EncodeNumber(Convert.ToInt32(textBox2.Text));
            lat_output_text += " ";
            lat_output_text += EncodeNumber(Convert.ToInt32(textBox3.Text));

            long_output_text += EncodeNumber(Convert.ToInt32(textBox6.Text));
            long_output_text += " ";
            long_output_text += EncodeNumber(Convert.ToInt32(textBox5.Text));
            long_output_text += " ";
            long_output_text += EncodeNumber(Convert.ToInt32(textBox4.Text));


            label9.Text = lat_output_text;
            label10.Text = long_output_text;
        }

        private void button2_Click(object sender, EventArgs e) // Decode button
        {
            string lat_output_text = "";
            string long_output_text = "";


            lat_output_text += DecodeWord(textBox7.Text).ToString();
            lat_output_text += " ";
            lat_output_text += DecodeWord(textBox8.Text).ToString();
            lat_output_text += " ";
            lat_output_text += DecodeWord(textBox9.Text).ToString();

            long_output_text += DecodeWord(textBox10.Text).ToString();
            long_output_text += " ";
            long_output_text += DecodeWord(textBox11.Text).ToString();
            long_output_text += " ";
            long_output_text += DecodeWord(textBox12.Text).ToString();

            label16.Text = lat_output_text;
            label17.Text = long_output_text;
        }

        bool IsNumeric(string input)
        {
            return Regex.IsMatch(input, @"^\d+$");
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
