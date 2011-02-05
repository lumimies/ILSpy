﻿// Copyright (c) 2011 AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace ICSharpCode.ILSpy.Disassembler
{
	public class ILLanguage : Language
	{
		bool detectControlStructure;
		
		public ILLanguage(bool detectControlStructure)
		{
			this.detectControlStructure = detectControlStructure;
		}
		
		public override string Name {
			get { return detectControlStructure ? "IL (structured)" : "IL"; }
		}
		
		public override ICSharpCode.AvalonEdit.Highlighting.IHighlightingDefinition SyntaxHighlighting {
			get { return ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinition("ILAsm"); }
		}
		
		public override void DecompileMethod(MethodDefinition method, ITextOutput output, DecompilationOptions options)
		{
			new ReflectionDisassembler(output, detectControlStructure, options.CancellationToken).DisassembleMethod(method);
		}
		
		public override void DecompileField(FieldDefinition field, ITextOutput output, DecompilationOptions options)
		{
			new ReflectionDisassembler(output, detectControlStructure, options.CancellationToken).DisassembleField(field);
		}
		
		public override void DecompileProperty(PropertyDefinition property, ITextOutput output, DecompilationOptions options)
		{
			new ReflectionDisassembler(output, detectControlStructure, options.CancellationToken).DisassembleProperty(property);
		}
		
		public override void DecompileEvent(EventDefinition ev, ITextOutput output, DecompilationOptions options)
		{
			new ReflectionDisassembler(output, detectControlStructure, options.CancellationToken).DisassembleEvent(ev);
		}
		
		public override void DecompileType(TypeDefinition type, ITextOutput output, DecompilationOptions options)
		{
			new ReflectionDisassembler(output, detectControlStructure, options.CancellationToken).DisassembleType(type);
		}
		
		public override void DecompileNamespace(string nameSpace, IEnumerable<TypeDefinition> types, ITextOutput output, DecompilationOptions options)
		{
			new ReflectionDisassembler(output, detectControlStructure, options.CancellationToken).DisassembleNamespace(nameSpace, types);
		}
		
		public override void DecompileAssembly(AssemblyDefinition assembly, ITextOutput output, DecompilationOptions options)
		{
			new ReflectionDisassembler(output, detectControlStructure, options.CancellationToken).WriteAssemblyHeader(assembly);
		}
		
		public override string TypeToString(TypeReference t)
		{
			PlainTextOutput output = new PlainTextOutput();
			t.WriteTo(output, true, true);
			return output.ToString();
		}
	}
}