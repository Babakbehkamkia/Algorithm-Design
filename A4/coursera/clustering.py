#Uses python3
import sys
import math

# def finding_roots(edges,k):
#     roots=[]
#     num_color=k
#     i=1
#     edge=edges[-i]
#     edge.s.color=0
#     edge.e.color=1
#     roots.append(edge.s)
#     roots.append(edge.e)
#     i+=1
#     while num_color>0:
#         edge=edges[-i]
#         d1=distance(edge.s.x,edge.s.y,edge.e.x,edge.e.y)
#         d2=distance(edge.s.x,edge.s.y,edge.e.x,edge.e.y)



def find(node):
    old_parent=node
    if old_parent.parent==-1:
        node.rank=0
        return node
    while old_parent.parent!=-1:
        old_parent=old_parent.parent
    
    node.rank=1
    node.parent=old_parent
    return old_parent

def union(s,e):
    
    e.parent=s
    e.rank+=1

def distance(x1,y1,x2,y2):
    return ((x1-x2)**2+(y1-y2)**2)**0.5

class Node:
    def __init__(self,x,y,parent):
        self.x=x
        self.y=y
        self.parent=parent
        self.rank=0
        # self.color

class Edge:
    def __init__(self,value,s,e):
        self.value=value
        self.s=s
        self.e=e
    def __lt__(self,other):
        return self.value<other.value
    def __eq__(self,other):
        return self.value==other.value

def clustering(x, y, k):
    nodes=[]
    for i in range(len(x)):
        node=Node(x[i],y[i],-1)
        nodes.append(node)
    edges=[]
    for i in range(len(x)):
        for j in range(i+1,len(x)):
            edge=Edge(distance(x[i],y[i],x[j],y[j]),nodes[i],nodes[j])
            edges.append(edge)
    edges.sort()
    union_num = 0
    # roots=finding_toots(edges,k)
    while len(edges):
        edge=edges.pop(0)
        a=find(edge.s)
        b=find(edge.e)
        if a!=b:
            union_num += 1
            union(a,b)
        if(union_num > len(nodes) - k):
            return edge.value
    #write your code here
    return -1.


if __name__ == '__main__':
    input = sys.stdin.read()
    # no_of_lines = 10
    # lines = ""
    # for i in range(no_of_lines):
    #     lines+=input()+"\n"
    # input=lines
    data = list(map(int, input.split()))
    n = data[0]
    data = data[1:]
    x = data[0:2 * n:2]
    y = data[1:2 * n:2]
    data = data[2 * n:]
    k = data[0]
    print("{0:.9f}".format(clustering(x, y, k)))
