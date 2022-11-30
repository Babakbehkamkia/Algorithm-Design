#Uses python3
import sys
class Node:
    def __init__(self,key,parent,edges):
        self.key=key
        self.parent=parent
        self.edges=edges

class Edge:
    def __init__(self,value,s,e):
        self.value=value
        self.s=s
        self.e=e
    def __eq__(self,other):
        return self.value==other.value
# Return the trie built from patterns
# in the form of a dictionary of dictionaries,
# e.g. {0:{'A':1,'T':2},1:{'C':3}}
# where the key of the external dictionary is
# the node ID (integer), and the internal dictionary
# contains all the trie edges outgoing from the corresponding
# node, and the keys are the letters on those edges, and the
# values are the node IDs to which these edges lead.
def build_trie(patterns):
    tree = dict()
    # write your code here
    n=len(patterns)
    nodes=[]
    root=Node(0,-1,[])
    nodes.append(root)
    index=1
    for i in range(n):
        current_parent=root
        for j in range(len(patterns[i])):
            letter=patterns[i][j]
            n=Node(index,current_parent,[])
            
            edge=Edge(letter,current_parent,n)
            is_exist=False
            for item in current_parent.edges:
                if edge==item:
                    n=item.e
                    is_exist=True
            if not is_exist:
                current_parent.edges.append(edge)
                nodes.append(n)
                index+=1
            current_parent=n
            
    for node in nodes:
        tree[node.key]={}
        for edge in node.edges:
            tree[node.key][edge.value]=edge.e.key


    return tree


if __name__ == '__main__':
    patterns = sys.stdin.read().split()[1:]
    tree = build_trie(patterns)
    for node in tree:
        for c in tree[node]:
            print("{}->{}:{}".format(node, tree[node][c], c))
