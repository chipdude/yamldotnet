using System;
using YamlDotNet.Core;
using System.Collections.Generic;

namespace YamlDotNet.RepresentationModel
{
    /// <summary>
    /// Represents an alias node in the YAML document.
    /// </summary>
    internal class YamlAliasNode : YamlNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YamlAliasNode"/> class.
        /// </summary>
        /// <param name="anchor">The anchor.</param>
        internal YamlAliasNode(string anchor)
        {
            Anchor = anchor;
        }

        /// <summary>
        /// Resolves the aliases that could not be resolved when the node was created.
        /// </summary>
        /// <param name="state">The state of the document.</param>
        internal override void ResolveAliases(DocumentState state)
        {
            throw new NotSupportedException("Resolving an alias on an alias node does not make sense");
        }
    }
}