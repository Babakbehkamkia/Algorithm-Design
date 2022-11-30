# python3
import sys


def build_suffix_tree(text):
  """
  Build a suffix tree of the string text and return a list
  with all of the labels of its edges (the corresponding 
  substrings of the text) in any order.
  """
  result = []
  # Implement this function yourself
  # return result
  node_list= f2(f1(text))
  for node in node_list:
    if node.parent_edge!=None:
      result.append(node.parent_edge.value)
  return result

class Node:
    def __init__(self,key,parent,edges):
        self.key=key
        self.parent=parent
        self.parent_edge=None
        self.edges=edges
        self.path_end=False

class Edge:
    def __init__(self,value,s,e):
        self.value=value
        self.s=s
        self.e=e
    def __eq__(self,other):
        if other==None:
            return False
        return self.value==other.value


def f1(text):
    nodes=[]
    root=Node(None,-1,[])
    nodes.append(root)
    # index=1
    for i in range(len(text)):
        current_parent=root
        for j in range(i,len(text)):
            letter=text[j]
            n=Node(None,current_parent,[])
            
            edge=Edge(letter,current_parent,n)
            is_exist=False
            for item in current_parent.edges:
                if edge==item:
                    n=item.e
                    is_exist=True
            if not is_exist:
                current_parent.edges.append(edge)
                n.parent_edge=edge
                if j==len(text)-1:
                    n.key=i
                nodes.append(n)
                    
                # index+=1
            current_parent=n
    return nodes


def f2(nodes):
    new_nodes=[]
    for node in nodes:
        if node.key!=None:
            while node.parent!=-1 and node.parent.parent!=-1 and len(node.parent.edges)<2:
                node.parent.parent_edge.value = node.parent.parent_edge.value + node.parent.edges[0].value
                node.parent.key=node.key
                for j in range(len(node.parent.parent.edges)):
                    if node.parent.parent.edges[j]==node.parent.parent_edge:
                        node.parent.parent.edges[j]=node.parent.parent_edge
                node=node.parent
            if not new_nodes.__contains__(node):
                new_nodes.append(node)
        if len(node.edges)>1:
            new_nodes.append(node)
    return new_nodes


if __name__ == '__main__':
  # text = sys.stdin.readline().strip()
  result = build_suffix_tree("ACACAA$")
  print("\n".join(result))