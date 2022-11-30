#python3
import sys

def compare(left,right):
    if left[-1]==right[0]:
        return 1
    return 0


def DFS(nodes,adj,start_index,index,depth):
    path=[index]
    if depth>0:
        for i in adj[index]:
            path+=DFS(nodes,adj,index,i,depth-1)
    return path


def find_bubble(nodes,adj,index,t):
    paths=[]
    for i in adj[index]:
        paths.append(DFS(nodes,adj,index,i,t-1))
    return paths

inputs=sys.stdin.read().split()
k,t,patterns=int(inputs[0]),int(inputs[1]),inputs[2:]


adj={}
for p in patterns:
    nodes=[]
    for index in range(len(p)-k+2):
        node=p[index:index+k-1]
        nodes.append(node)
    for i in range(len(nodes)-1):
        if nodes[i] not in adj:
            adj[nodes[i]]=[]
            # if i==j:
            #     continue
        if compare(nodes[i],nodes[i+1])==1:
            if nodes[i+1] not in adj[nodes[i]]:
                adj[nodes[i]].append(nodes[i+1])



count=0
for item in adj:
    paths=[]
    if len(adj[item])>1:
        paths=find_bubble(nodes,adj,item,t)
    if len(paths):
        for i in range(len(paths)):
            for j in range(i+1,len(paths)):
                if i==j:
                    continue
                if paths[i][-1]==paths[j][-1]:
                    is_valid=True
                    for k in range(len(paths[i])-1):
                        if paths[i][k]==paths[j][k]:
                            is_valid=False
                    if is_valid:
                        count+=1



print(count)