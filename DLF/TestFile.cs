using System;
using System.Collections.Generic;
using DLFramework;
using LinearAlgebra;

class TestFile {
    static void TestExpand () {
        var data = new Tensor ((Matrix) new double[, ] { { 1 } });
        Console.WriteLine ($"Shape weight {Tensor.Expand(data, AxisZero.horizontal, 4).Data}");
    }

    static void Test1 () {
        //TEST

        var x = new Tensor ((Matrix) new double[, ] { { 1, 2, 3, 4, 5 } }, true);
        var y = new Tensor ((Matrix) new double[, ] { { 1, 1, 1, 1, 1 } }, true);

        var z = Tensor.Add (x, y);

        //Sum
        Console.WriteLine ("Matrix a + 1, d");
        Console.WriteLine (z.ToString ());

        //Backward
        Console.WriteLine ("Grad x before");
        Console.WriteLine (x.Gradient);

        Console.WriteLine ("Grad y before");
        Console.WriteLine (y.Gradient);

        z.Backward (new Tensor ((Matrix) new double[, ] { { 1, 1, 1, 1, 1 } }));
        Console.WriteLine ("Creators");
        foreach (var creator in z.Creators) {
            Console.WriteLine (creator.ToString ());
        }
        Console.WriteLine ("Operation");
        Console.WriteLine (z.CreationOperation);

        Console.WriteLine ("Grad x");
        Console.WriteLine (x.Gradient);

        Console.WriteLine ("Grad y");
        Console.WriteLine (y.Gradient);

        //Backward Stress
        var a = new Tensor ((Matrix) new double[, ] { { 1, 2, 3, 4, 5 } }, true);
        var b = new Tensor ((Matrix) new double[, ] { { 2, 2, 2, 2, 2 } }, true);
        var c = new Tensor ((Matrix) new double[, ] { { 5, 4, 3, 2, 1 } }, true);

        var d = Tensor.Add (a, b);
        var e = Tensor.Add (b, c);

        var f = Tensor.Add (d, e);

        f.Backward (new Tensor ((Matrix) new double[, ] { { 1, 1, 1, 1, 1 } }));
        Console.WriteLine ("Grad b");
        Console.WriteLine (b.Gradient);

        //Negation
        var a2 = new Tensor ((Matrix) new double[, ] { { 1, 2, 3, 4, 5 } }, true);
        var b2 = new Tensor ((Matrix) new double[, ] { { 2, 2, 2, 2, 2 } }, true);
        var c2 = new Tensor ((Matrix) new double[, ] { { 5, 4, 3, 2, 1 } }, true);

        var d2 = Tensor.Add (a2, Tensor.Neg (b2));
        var e2 = Tensor.Add (Tensor.Neg (b2), c2);

        var f2 = Tensor.Add (d2, e2);
        f2.Backward (new Tensor ((Matrix) new double[, ] { { 1, 1, 1, 1, 1 } }));

        Console.WriteLine ("Grad b2");
        Console.WriteLine (b2.Gradient);
    }
}