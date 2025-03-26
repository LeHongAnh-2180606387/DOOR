using System;
using System.Collections;
using System.Collections.Generic;

namespace BehaviorTree
{
    // Định nghĩa các trạng thái có thể của một Node trong Behavior Tree
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    // Định nghĩa lớp cơ sở cho các Node trong Behavior Tree
    public class Node
    {
        // Trạng thái hiện tại của Node
        protected NodeState state;

        // Node cha của Node hiện tại
        public Node parent;

        // Danh sách các Node con của Node hiện tại
        protected List<Node> children = new List<Node>();

        // Dữ liệu ngữ cảnh của Node
        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        // Constructor không tham số, khởi tạo Node mà không có Node cha
        public Node()
        {
            parent = null;
        }

        // Constructor nhận danh sách các Node con và gán chúng cho Node hiện tại
        public Node(List<Node> children)
        {
            foreach(Node child in children)
                _Attach(child);
        }

        // Phương thức riêng để gán Node cha và thêm Node con vào danh sách các Node con
        private void _Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        // Phương thức ảo để đánh giá Node, mặc định trả về trạng thái FAILURE
        public virtual NodeState Evaluate() => NodeState.FAILURE;

        // Phương thức để thiết lập dữ liệu ngữ cảnh cho Node
        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        // Phương thức để lấy dữ liệu ngữ cảnh của Node
        public object GetData(string key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
                return value;
            Node node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.parent;
            }
            return null;
        }

        // Phương thức để xóa dữ liệu ngữ cảnh của Node
        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }
            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }
    }
}
