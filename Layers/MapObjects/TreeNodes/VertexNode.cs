using System;
using System.Data;
using ProgramMain.ExampleDb;
using ProgramMain.Map;
using ProgramMain.Map.Spatial.Types;

namespace ProgramMain.Layers.MapObjects.TreeNodes
{
    public class VertexNode : ISpatialTreeNode
    {
        private readonly SimpleMapDb.VertexesRow _row;

        public SpatialTreeNodeTypes NodeType
        {
            get { return SpatialTreeNodeTypes.Point; }
        }

        public GeomCoordinate Coordinate
        {
            get { return new GeomCoordinate(_row.Longitude, _row.Latitude); }
        }

        public CoordinateRectangle Rectangle
        {
            get { throw new Exception(); }
        }

        public CoordinatePoligon Poligon
        {
            get { throw new Exception(); }
        }

        public int RowId
        {
            get { return _row.ID; }
        }

        internal VertexNode(SimpleMapDb.VertexesRow row)
        {
            _row = row;    
        }

        public DataRow Row
        {
            get { return _row; }
        }

        public static implicit operator VertexNode(SimpleMapDb.VertexesRow row)
        {
            return new VertexNode(row);
        }
    }
}