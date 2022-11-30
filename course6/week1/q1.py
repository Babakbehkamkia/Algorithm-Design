#python3
import sys
from timeit import default_timer as timer


def addVertex(nodes,new_node,adj):
    adj[new_node]={}
    for i in range(len(nodes)):
        addEdge(new_node,nodes[i],adj)
        addEdge(nodes[i],new_node,adj)
    nodes.append(new_node)



def addEdge(left,right,adj):
    s=compare(left,right)
    adj[left][right]=s


def compute_suffix(str):
    s=[0 for i in range(len(str))]
    counter=0
    for i in range(1,len(str)):
        while counter>0 and str[i]!=str[counter]:
            counter=s[counter-1]
        if str[i]==str[counter]:
            counter+=1
        s[i]=counter
    return s[-1]


def compare(right,left):
    for i in range(12,0,-1):
        if left[:i]==right[len(right)-i:]:
            return i
    return 0
nodes=[]
for i in range(1618):
    m=input()
    if m not in nodes:
        nodes.append(m)
# start2=timer()
index=0
lens=[]
# path=[]
result=""
first_node=nodes[0]
result+=first_node
while(len(nodes)>1):
    current_node=nodes.pop(index)
    m=-1
    # m_index=-1
    for j in range(len(nodes)):
        compare_node=nodes[j]
        # new_str=compare_node+'$'+current_node
        # s=compute_suffix(new_str)
        s=compare(current_node,compare_node)
        if s>=m:
            index=j
            m=s
    result+=nodes[index][m:]

last_node=compare_node

s=compare(last_node,first_node)
result=result[:-s]
# end=timer()
print(result)