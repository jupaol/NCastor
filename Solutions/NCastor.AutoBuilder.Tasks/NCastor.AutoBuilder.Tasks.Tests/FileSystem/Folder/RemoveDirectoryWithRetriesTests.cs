// -----------------------------------------------------------------------
// <copyright file="RemoveDirectoryWithRetriesTests.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// Copyright (c) 2012, Juan Pablo Olmos Lara (Jupaol)
// All rights reserved.
// 
// </copyright>
// -----------------------------------------------------------------------

namespace NCastor.AutoBuilder.Tasks.Tests.FileSystem.Folder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using NCastor.AutoBuilder.Tasks.FileSystem.Folder;
using Machine.Specifications;
using Machine.Fakes;
using Microsoft.Build.Tasks;
    using Microsoft.Build.Framework;
    using FluentAssertions;
using Microsoft.Build.Utilities;
    using System.IO;
using System.Reflection;
using System.Reflection.Emit;

    public class RemoveDirectoryFake : RemoveDir
    {
        public override bool Execute()
        {
            return false;
        }
    }

    [Subject(typeof(RemoveDirectoryWithRetries))]
    public class can_create_a_new_RemoveDirectoryWithRetries_object : WithSubject<RemoveDirectoryWithRetries>
    {
        It should_create_a_new_RemoveDirectoryWithRetries_object = () =>
        {
            Subject.Should().NotBeNull();
        };
    }

    [Subject(typeof(RemoveDirectoryWithRetries))]
    public class can_setup_the_number_of_retries_correctly : WithSubject<RemoveDirectoryWithRetries>
    {
        Establish context = () =>
        {
            Subject.RetryCount = 10;
        };

        It should_be_able_to_read_the_number_of_retries_specified = () =>
        {
            Subject.RetryCount.Should().Be(10);
        };
    }
    public class kk
    {
        public bool IsValid()
        {
            return false;
        }
    }

    public class jj : kk
    {
        new public bool IsValid()
        {
            return true;
        }
    }

    [Subject(typeof(RemoveDirectoryWithRetries))]
    [Ignore("Func is nto used")]
    public class cc
    {
        Establish context = () =>
        {

        };

        It should = () =>
        {
            new jj().IsValid().Should().BeTrue();
            new RemoveDirectoryWithRetries().Execute();
        };
    }

    public class A
    {
        public string Print()
        {
            return "A";
        }
    }

    public class B : A
    {
        public new virtual string Print()
        {
            return "B";
        }
    }

    public class C : B
    {
        public C()
        {
            string name = "NCastor.AutoBuilder.Tasks.Tests";
            AssemblyName an = new AssemblyName(name);
            AssemblyBuilder ab = AppDomain.CurrentDomain.DefineDynamicAssembly(
                an, AssemblyBuilderAccess.Run);

            ModuleBuilder mb = ab.DefineDynamicModule(name, name + ".dll");

            TypeBuilder tb =
                mb.DefineType("C", TypeAttributes.Public, typeof(A));

            MethodBuilder meb = tb.DefineMethod(
                "Print", 
                MethodAttributes.Virtual | MethodAttributes.Public | MethodAttributes.NewSlot, null, Type.EmptyTypes);



            MethodInfo m = typeof(A).GetMethod("Print", BindingFlags.Public);
        }

        public new string Print()
        {
            return "C";
        }
    }

    [Ignore("Func nto used")]
    public class PolyTest
    {
        It shouuld = () =>
        {
            new A().Print().Should().Be("A");
            new B().Print().Should().Be("B");

            A a = new C();

            a.Print().Should().Be("C");
        };
    }
}
