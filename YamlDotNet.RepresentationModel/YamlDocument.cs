﻿using System;
using YamlDotNet.Core;
using System.Diagnostics;
using System.Collections.Generic;

namespace YamlDotNet.RepresentationModel
{
    /// <summary>
    /// Represents an YAML document.
    /// </summary>
    public class YamlDocument
    {
        private YamlNode rootNode;

        /// <summary>
        /// Gets or sets the root node.
        /// </summary>
        /// <value>The root node.</value>
        public YamlNode RootNode
        {
            get
            {
                return rootNode;
            }
            set
            {
                rootNode = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YamlDocument"/> class.
        /// </summary>
        public YamlDocument()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YamlDocument"/> class.
        /// </summary>
        /// <param name="events">The events.</param>
        internal YamlDocument(EventReader events)
        {
            DocumentState state = new DocumentState();

            events.Expect<DocumentStartEvent>().Dispose();

            while (!events.Accept<DocumentEndEvent>())
            {
                Debug.Assert(rootNode == null);
                rootNode = YamlNode.ParseNode(events, state);

                if (rootNode is YamlAliasNode)
                {
                    throw new YamlException();
                }
            }

            if (state.HasUnresolvedAliases)
            {
                rootNode.ResolveAliases(state);
            }

            events.Expect<DocumentEndEvent>().Dispose();
        }
    }
}