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


# nodes=[]
# adj={}
# # input = sys.stdin.read().split()
# for k in range(1618):
#     new_node=input()
#     addVertex(nodes,new_node,adj)


# path=[]
# count=0
# v={}
# for i in adj[nodes[0]]:
#     v[i]=False
# path.append([nodes[0],v])
# is_finished=False
# while(True):
#     item=path[-1]
#     current_node=item[0]
#     visited=item[1]
#     isChosen=False
#     for i in adj[current_node]:
#         if adj[current_node][i]>0 and not isChosen:
#             if i==nodes[0]:
#                 if count==len(nodes)-1:
#                     is_finished=True
#                     break
#                 else:
#                     continue
#             if visited[i]==False:
#                 isExist=False
#                 for n in path:
#                     if n[0] ==i:
#                         isExist=True
#                         break
#                 if not isExist:
#                     isChosen=True
#                     visited[i]=True
#                     v={}
#                     for m in adj[nodes[0]]:
#                         v[m]=False
#                     path.append([i,v])
#                     count+=1
#     if is_finished:
#         break
#     if not isChosen:
#         trash=path.pop()
#         count-=1
    


# result=""
# for i in range(0,len(path)-1):
#     current_node=path[i][0]
#     next_node=path[i+1][0]
#     result+=current_node[:len(current_node)-adj[current_node][next_node]]

# current_node=path[-1][0]
# first_node=path[0][0]
# result+=current_node[:len(current_node)-adj[current_node][first_node]]
# print(result)

# start1=timer()
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

# for i in range(15):   
#     current_node=nodes.pop(index)
#     # path.append(current_node)

#     m=0
#     m_index=-1
#     for j in range(len(nodes)):
#         compare_node=nodes[j]
#         # new_str=compare_node+'$'+current_node
#         # s=compute_suffix(new_str)
#         s=compare(current_node,compare_node)
#         if s>=m:
#             m_index=j
#             m=s
#     index=m_index
#     # lens.append(m)
    
#     result+=nodes[m_index][m:]

last_node=compare_node

s=compare(last_node,first_node)
result=result[:-s]
# end=timer()
print(result)
# print(start2-start1)
# print(end-start2)
# print(end-start1)


# result=""
# for i in range(0,len(path)-1):
#     current_node=path[i]
#     result+=current_node[:len(current_node)-lens[i]]

# # new_str=path[0]+'$'+path[-1]
# # s=compute_suffix(new_str)
# s=compare(path[-1,path[0]])
# current_node=path[-1]
# first_node=path[0]
# result+=current_node[:len(current_node)-s]
# print(result)