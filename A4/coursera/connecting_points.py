#Uses python3
import sys
import math
import queue

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
    # if s.rank>e.rank:
    #     if e.parent==-1:
    #         s.parent.parent=e
    #         s.parent.rank+=1
    #     else:
    #         s.parent.parent=e.parent
    #         s.parent.rank+=1
    #     # s.rank=s.parent.rank+1
    #     # e.rank=e.parent.rank+1
    # elif s.rank<e.rank:
    #     if s.parent==-1:
    #         e.parent.parent=s
    #         e.parent.rank+=1
    #     else:
    #         e.parent.parent=s.parent
    #         e.parent.rank+=1
    #     # s.rank=s.parent.rank+1
    #     # e.rank=e.parent.rank+1
    # else:
    #     if s.parent==-1:
    #         s.parent=e
    #         s.rank+=1
    #     else:
    #         s.parent.parent=e.parent
    #         s.parent.rank+=1
    #         s.rank+=1
    
    # return (s,e)


def distance(x1,y1,x2,y2):
    return ((x1-x2)**2+(y1-y2)**2)**0.5

class Node:
    def __init__(self,x,y,parent):
        self.x=x
        self.y=y
        self.parent=parent
        self.rank=0

class Edge:
    def __init__(self,value,s,e):
        self.value=value
        self.s=s
        self.e=e
    def __lt__(self,other):
        return self.value<other.value
    def __eq__(self,other):
        return self.value==other.value

def minimum_distance(x, y):
    result = 0.
    #write your code here
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
    while len(edges):
        edge=edges.pop(0)
        # edge.s.parent=find(edge.s)
        # edge.e.parent=find(edge.e)
        a=find(edge.s)
        b=find(edge.e)
        if a!=b:
            union(a,b)
            result+=edge.value
    return result


if __name__ == '__main__':
    input = sys.stdin.read()
    # no_of_lines = 6
    # lines = ""
    # for i in range(no_of_lines):
    #     lines+=input()+"\n"
    # input=lines
    data = list(map(int, input.split()))
    n = data[0]
    x = data[1::2]
    y = data[2::2]
    print("{0:.9f}".format(minimum_distance(x, y)))
