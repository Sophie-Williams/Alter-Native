﻿// 
// IdentifierExpression.cs
//  
// Author:
//       Mike Krüger <mkrueger@novell.com>
// 
// Copyright (c) 2009 Novell, Inc (http://www.novell.com)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System.Collections.Generic;
using ICSharpCode.NRefactory.Cpp.Ast;

namespace ICSharpCode.NRefactory.Cpp
{
	public class IdentifierExpression : Expression
	{
		public IdentifierExpression()
		{
            //base.generateNewId();
		}
		
		public IdentifierExpression(string identifier)
		{
			this.Identifier = identifier;
		}
		
		public IdentifierExpression(string identifier, TextLocation location)
		{
			SetChildByRole(Roles.Identifier, Ast.Identifier.Create (identifier, location));
		}
		
//		public Identifier IdentifierToken {
//			get { return GetChildByRole (Roles.Identifier); }
//		}
		
		public string Identifier {
			get {
				return GetChildByRole (Roles.Identifier).Name;
			}
			set {
				SetChildByRole(Roles.Identifier, Ast.Identifier.Create (value, TextLocation.Empty));
			}
		}
		
		public AstNodeCollection<AstType> TypeArguments {
			get { return GetChildrenByRole (Roles.TypeArgument); }
		}
		
		public override S AcceptVisitor<T, S> (IAstVisitor<T, S> visitor, T data = default(T))
		{
			return visitor.VisitIdentifierExpression (this, data);
		}
		
		protected internal override bool DoMatch(AstNode other, PatternMatching.Match match)
		{
			IdentifierExpression o = other as IdentifierExpression;
			return o != null && MatchString(this.Identifier, o.Identifier) && this.TypeArguments.DoMatch(o.TypeArguments, match);
		}

        public override string ToString()
        {
            return Identifier;
        }
	}
}