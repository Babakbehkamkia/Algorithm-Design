#python3

import sys
from typing import Pattern
from timeit import default_timer as timer

class Node:
    def __init__(self,key,parent,edges,reads):
        self.key=key
        self.parent=parent
        self.edges=edges
        self.reads=reads

class Edge:
    def __init__(self,value,s,e):
        self.value=value
        self.s=s
        self.e=e
    def __eq__(self,other):
        return self.value==other.value

def explore(nodes,pattern,visited):
    parent_node=nodes[0]
    for p in pattern:
        isFound=False
        for i in range(len(parent_node.edges)):
            if p==parent_node.edges[i].value:
                isFound=True
                parent_node=parent_node.edges[i].e
                break
        if not isFound:
            return None
    for i in range(len(parent_node.reads)):
        if parent_node.reads[i]!=pattern and visited[parent_node.reads[i]]==0:
            visited[parent_node.reads[i]]=1
            return parent_node.reads[i]


def compare(nodes,pattern,visited):
    new_p=None
    for i in range(len(pattern)-12,len(pattern)):
        p=pattern[i:len(pattern)]
        new_p=explore(nodes,p,visited)
        if new_p!=None:
            return new_p,len(pattern)-i





def build_trie(patterns):
    # tree = dict()
    # write your code here
    n=len(patterns)
    nodes=[]
    root=Node(0,-1,[],[])
    nodes.append(root)
    index=1
    for i in range(n):
        current_parent=root
        for j in range(len(patterns[i])):
            letter=patterns[i][j]
            n=Node(index,current_parent,[],[patterns[i]])
            
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
            if patterns[i] not in current_parent.reads:
                current_parent.reads.append(patterns[i])
    return nodes


if __name__ == '__main__':
    # start1=timer()
    patterns=[]
    visited={}
    for i in range(1618):
        n=input()
        visited[n]=0
        patterns.append(n)
    # start2=timer()
    # for i in patterns:
    #     visited[i]=0
    # start3=timer()
    nodes = build_trie(patterns)
    
    pattern=patterns[0]
    result=pattern
    for i in range(1617):
        # if i==1551:
        #     a=0
        pattern,s=compare(nodes,pattern,visited)
        result+=pattern[s:]
    p,s=compare(nodes,pattern,visited)
    print(result[:-(len(pattern)-s+1)])
    # end=timer()
    # print(end-start1)
    # print(start2-start1)
    # print(start3-start2)
    # print(end-start2)
