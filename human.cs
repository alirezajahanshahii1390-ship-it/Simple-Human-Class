using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Odbc;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace main_project
{
    class human
    {

        public string name { get; set; }
        public string family { get; set; }
        public byte age { get; set; }
        public int id { get; set; }
        public bool exist(string name , string family)
        {
            var q = from i in (new db().humans).ToList() where i.name == name && i.family == family select i;
            if (q.Count() >= 1)
            {
                return true;
            }
            return false;
        }
        public List<human> search(string text)
        {

            var q = from i in (new db().humans).ToList() where i.name.Contains(text) || i.family.Contains(text) select i;
            return q.ToList();
        }
        public bool creat(string name, string family , byte age)
        {
            if (age >= 18 && exist(name,family) != true)
            {
                db db1 = new db();
                db1.humans.Add(new human { name = name , family = family , age = age});
                db1.SaveChanges();
                return true;
            }
            return false;
        }
        public void update(string name , string family , byte age , int id)
        {
            if (exist(name, family) != true)
            {
                if (age >= 18)
                {
                    human human = new human();

                    human.name = name;

                    human.family = family;

                    human.age = age;

                    db db1 = new db();

                    var q = from i in db1.humans where i.id == id select i;

                    q.Single().name = human.name;
                    q.Single().family = human.family;
                    q.Single().age = human.age;

                    db1.SaveChanges();
                }
            }
        }
        public human search(int id)
        {
            db db1  = new db();
            var q = from i in db1.humans.ToList() where i.id == id select i;
            return q.Single();
        }
    }
}




