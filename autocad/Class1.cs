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
    [AttributeUsage(AttributeTargets.Method)]

    public class PaletteMethod : Attribute { }



    public class Commands
    {

        private static PaletteSet _ps = null;

        private static Document doc = Application.DocumentManager.MdiActiveDocument;

        private static Editor ed = doc.Editor;

        private static Database db = doc.Database;


        [PaletteMethod]

        [CommandMethod("asola")]

        public void PaletteCcommands()

        {

            if (_ps == null)

            {

                if (doc == null) return;

                doc.LockDocument();

                var asm = Assembly.GetExecutingAssembly();

                var type = asm.GetType("ModelessDialogs.Commands");

                _ps = new PaletteSet("PC", new Guid("87374E16-C0DB-4F3F-9271-7A71ED921566"));


                var uc = new UserControl1();

                _ps.Add("CMDPAL", uc);

                _ps.MinimumSize = new Size(200, (1 + 1) * 40);

                _ps.DockEnabled = (DockSides)(DockSides.Left | DockSides.Right);

            }



            _ps.Visible = true;

        }

        public void DrawProfileCommand(int lunghezza, int spessore)
        {

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
        
        [CommandMethod("LockDoc", CommandFlags.Session)]
        private void LockDoc()
        {

            // Create a new drawing

            DocumentCollection acDocMgr = Application.DocumentManager;

            Document acNewDoc = acDocMgr.Add("acad.dwt");

            Database acDbNewDoc = acNewDoc.Database;



            // Lock the new document

            using (DocumentLock acLckDoc = acNewDoc.LockDocument())

            {

                // Start a transaction in the new database

                using (Transaction acTrans = acDbNewDoc.TransactionManager.StartTransaction())

                {

                    // Open the Block table for read

                    BlockTable acBlkTbl;

                    acBlkTbl = acTrans.GetObject(acDbNewDoc.BlockTableId,

                                                 OpenMode.ForRead) as BlockTable;



                    // Open the Block table record Model space for write

                    BlockTableRecord acBlkTblRec;

                    acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],

                                                    OpenMode.ForWrite) as BlockTableRecord;



                    // Create a circle with a radius of 3 at 5,5

                    Circle acCirc = new Circle();

                    acCirc.SetDatabaseDefaults();

                    acCirc.Center = new Point3d(5, 5, 0);

                    acCirc.Radius = 3;



                    // Add the new object to Model space and the transaction

                    acBlkTblRec.AppendEntity(acCirc);

                    acTrans.AddNewlyCreatedDBObject(acCirc, true);



                    // Save the new object to the database

                    acTrans.Commit();

                }



                // Unlock the document

            }



            // Set the new document current

            acDocMgr.MdiActiveDocument = acNewDoc;

        }
    }

}

