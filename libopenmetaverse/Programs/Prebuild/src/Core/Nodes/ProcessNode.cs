#region BSD License
/*
Copyright (c) 2004-2005 Matthew Holmes (matthew@wildfiregames.com), Dan Moorehead (dan05a@gmail.com)

Redistribution and use in source and binary forms, with or without modification, are permitted
provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list of conditions 
  and the following disclaimer. 
* Redistributions in binary form must reproduce the above copyright notice, this list of conditions 
  and the following disclaimer in the documentation and/or other materials provided with the 
  distribution. 
* The name of the author may not be used to endorse or promote products derived from this software 
  without specific prior written permission. 

THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, 
BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
ARE DISCLAIMED. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS
OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY
OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING
IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion

#region CVS Information
/*
 * $Source$
 * $Author: jendave $
 * $Date: 2006-01-27 16:49:58 -0800 (Fri, 27 Jan 2006) $
 * $Revision: 71 $
 */
#endregion

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;

using Prebuild.Core.Attributes;
using Prebuild.Core.Interfaces;
using Prebuild.Core.Utilities;

namespace Prebuild.Core.Nodes
{
	/// <summary>
	/// 
	/// </summary>
	[DataNode("Process")]
	public class ProcessNode : DataNode
	{
		#region Fields

		private string m_Path;
		private bool m_IsValid = true;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the path.
		/// </summary>
		/// <value>The path.</value>
		public string Path
		{
			get
			{
				return m_Path;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is valid.
		/// </summary>
		/// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
		public bool IsValid
		{
			get
			{
				return m_IsValid;
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Parses the specified node.
		/// </summary>
		/// <param name="node">The node.</param>
		public override void Parse(XmlNode node)
		{
			if( node == null )
			{
				throw new ArgumentNullException("node");
			}

			m_Path = Helper.InterpolateForEnvironmentVariables(node.InnerText);
			if(m_Path == null)
			{
				m_Path = "";
			}

			try
			{
				m_Path = Helper.ResolvePath(m_Path);
			}
			catch(ArgumentException)
			{
				Kernel.Instance.Log.Write(LogType.Warning, "Could not find prebuild file for processing: {0}", m_Path);
				m_IsValid = false;
			}
		}

		#endregion
	}
}
