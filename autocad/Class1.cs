using Autodesk.AutoCAD.ApplicationServices;

using Autodesk.AutoCAD.Runtime;

using Autodesk.AutoCAD.Windows;

using System;

using System.Collections.Generic;

using System.Drawing;

using System.Reflection;

using WinForms = System.Windows.Forms;

using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
namespace autocad

{

    // The basic attribute we'll use to tag the methods/commands to place

    // on the palette
    [AttributeUsage(AttributeTargets.Method)]

    public class PaletteMethod : Attribute { }



    public class Commands
    {

        private static PaletteSet _ps = null;


        // Our main command to display a palette. This is flagged to be included

        // on the palette, too, which is a bit silly, but harmless (as the

        // command doesn't do anything if the palette is already available)



        [PaletteMethod]

        [CommandMethod("asola")]

        public void PaletteCcommands()

        {

            if (_ps == null)

            {

                var doc = Application.DocumentManager.MdiActiveDocument;

                if (doc == null) return;

                var ed = doc.Editor;



                // We're going to take a look at the various methods in this module



                var asm = Assembly.GetExecutingAssembly();

                var type = asm.GetType("ModelessDialogs.Commands");

                //if (type == null)

                //{

                //    ed.WriteMessage("\nCould not find the command class.");

                //    return;

                //}



                // We'll create a sequence of buttons for each callable method



                //var bs = new List<WinForms.Button>();

                //var i = 1;



                //// Loop over each method



                //foreach (var m in type.GetMethods())

                //{

                //    var cmdName = "";

                //    var palette = false;



                //    // And then all of its attributes



                //    foreach (var a in m.CustomAttributes)

                //    {

                //        // Check whether we have a command and/or a "palette" attb



                //        if (a.AttributeType.Name == "CommandMethodAttribute")

                //        {

                //            cmdName = (string)a.ConstructorArguments[0].Value;

                //        }

                //        else if (a.AttributeType.Name == "PaletteMethod")

                //        {

                //            palette = true;

                //        }

                //    }



                //    // If we have a palette attb, then one way or another it'll be

                //    // added to the palette



                //    if (palette)

                //    {

                //        // Create our button and give it a position



                //        var b = new WinForms.Button();

                //        b.SetBounds(50, 40 * i, 100, 30);



                //        // If no command name was found, use the method name and call the

                //        // function directly in the session context



                //        if (String.IsNullOrEmpty(cmdName))

                //        {

                //            b.Text = m.Name;

                //            b.Click +=

                //              (s, e) =>

                //              {

                //                  var b2 = (WinForms.Button)s;

                //                  var mi = type.GetMethod(b2.Text);

                //                  if (mi != null)

                //                  {

                //                      // Use reflection to call the method with no arguments



                //                      mi.Invoke(this, null);

                //                  }

                //              };

                //        }

                //        else

                //        {

                //            // Otherwise we use the command name as the button text and

                //            // execute the command in the command context asynchronously



                //            b.Text = cmdName;

                //            b.Click +=

                //              async (s, e) =>

                //              {

                //                  var dm = Application.DocumentManager;

                //                  var doc2 = dm.MdiActiveDocument;

                //                  if (doc2 == null) return;



                //                  // We could also use SendStringToExecute for older versions



                //                  // doc2.SendStringToExecute(

                //                  //   "_." + cmdName + " ", false, false, true

                //                  // );



                //                  var ed2 = doc2.Editor;



                //                  await dm.ExecuteInCommandContextAsync(

                //      async (obj) =>

                //                  {

                //                      await ed2.CommandAsync("_." + cmdName);

                //                  },

                //      null

                //    );

                //              };

                //        }

                //        bs.Add(b);

                //        i++;

                //    }

                //}



                //// Create a user control and add all our buttons to it



                //var uc = new WinForms.UserControl();

                //uc.Controls.AddRange(bs.ToArray());



                // Create a palette set and add a palette containing our control



                _ps = new PaletteSet("PC", new Guid("87374E16-C0DB-4F3F-9271-7A71ED921566"));


                var uc = new UserControl1();

                _ps.Add("CMDPAL", uc);

                _ps.MinimumSize = new Size(200, (1 + 1) * 40);

                _ps.DockEnabled = (DockSides)(DockSides.Left | DockSides.Right);

            }



            _ps.Visible = true;

        }



        // Helper function to display a message and post a command prompt

        // (if there's an active document available)



        private static void DisplayMessage(string str, bool postPrompt = true)

        {

            var doc = Application.DocumentManager.MdiActiveDocument;

            if (doc == null) return;

            doc.Editor.WriteMessage("\n{0}\n", str);

            if (postPrompt)

                doc.Editor.PostCommandPrompt();

        }



        // A bunch of test methods and a single proper AutoCAD command

        // (it's a bit misleading to think of the

        [PaletteMethod]

        public void Method1()

        {

            DisplayMessage("Method 1");

        }



        [PaletteMethod]

        public void Method2()

        {

            DisplayMessage("Method 2");

        }



        [PaletteMethod]

        public void Method3()

        {

            DisplayMessage("Method 3");

        }



        [PaletteMethod]

        public void Method4()

        {

            DisplayMessage("Method 4");

        }



        [PaletteMethod]

        [CommandMethod("TEST")]

        public static void CommandTest()

        {

            DisplayMessage("This is a command!", false);

        }


        public void DrawProfileCommand(int lunghezza, int spessore)
        {
            var doc = Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;
            var ed = doc.Editor;

            var lunghezzaInt = lunghezza;
            var spessoreInt = spessore;

            //var spessore = ed.GetPoint("\nIserisci spessore: ");
            //var lunghezza = ed.GetPoint("\nInserisci lunghezza: ");
            var pbase = ed.GetPoint("\nPunto base: ");

            //Point3d ancora = new Point3d(pbase.Value.X - (lunghezzaInt / 2), pbase.Value.Y - (spessoreInt/2), 0); 
            Point3d ancora = new Point3d(pbase.Value.X - ((lunghezzaInt / 2) - (spessoreInt / 2)), pbase.Value.Y - (spessoreInt / 2), 0);

            using (var tr = db.TransactionManager.StartTransaction())
            {
                var curSpace = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);
                using (var pline = DrawProfile(lunghezzaInt, spessoreInt))
                {
                    // transform the polyline about current coordinate system and specified instertion point
                    pline.TransformBy(ed.CurrentUserCoordinateSystem * Matrix3d.Displacement(ancora.GetAsVector()));
                    // append the polyline to the current space
                    curSpace.AppendEntity(pline);
                    // add the polyline to the transaction
                    tr.AddNewlyCreatedDBObject(pline, true);
                }
                // save the changes to the database
                tr.Commit();
            }
        }
        private Polyline DrawProfile(int lunghezza, int spessore)
        {
            var pline = new Polyline(10);
            var pt = Point2d.Origin;
            pline.AddVertexAt(0, pt, 0.0, 0.0, 0.0);
            pt += new Vector2d(lunghezza - spessore, 0.0);
            pline.AddVertexAt(1, pt, 1, 0.0, 0.0);
            pt += new Vector2d(0.0, spessore);
            pline.AddVertexAt(2, pt, 0.0, 0.0, 0.0);
            pt += new Vector2d(-(lunghezza - spessore), 0);
            pline.AddVertexAt(3, pt, 1, 0.0, 0.0);
            pline.Closed = true;
            return pline;
        }
    }
}

