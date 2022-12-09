// See https://aka.ms/new-console-template for more information
using StringFormatter.Tests;

Console.WriteLine("Hello, World!");
ClassForTests _target = new ClassForTests();
StringFormatter.Core.StringFormatter.Shared.Format("Hi {} with password={Password}", _target);
