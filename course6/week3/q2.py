#python3


def compare(left,right,k):
    if left[-1]==right[0]:
        return 1
    return 0



patterns=[]

for i in range(4):
    patterns.append(input())

# for k in range(100,0,-1):
#     nodes=[]
#     for p in patterns:
#         for index in range(len(p)-k+1):
#             node=p[index:index+k]
#             if node not in nodes:
#                 nodes.append(node)

#     if len(nodes):
#         inputs=[0 for i in range(len(nodes))]
#         outputs=[0 for i in range(len(nodes))]
#         for i in range(len(nodes)):
#             for j in range(len(nodes)):
#                 if i==j:
#                     continue
#                 if compare(nodes[i],nodes[j])==1:
#                     inputs[j]+=1
#                     outputs[i]+=1
#         is_valid=True
#         for i in range(len(nodes)):
#             if inputs[i]!=outputs[i]:
#                 is_valid=False
#         if(is_valid):
#             print(k)
#             break


for k in range(100,1,-1):
    if k!=7:
        continue
    adj={}
    back_adj={}
    is_valid2=False
    for p in patterns:
        nodes=[]
        for index in range(len(p)-k+2):
            is_valid2=True
            node=p[index:index+k-1]
            nodes.append(node)
        for i in range(len(nodes)):
            if nodes[i] not in adj:
                adj[nodes[i]]=[]
            if nodes[i] not in back_adj:
                back_adj[nodes[i]]=[]
        for i in range(len(nodes)-1):
            for j in range(1,k-1):
            # if compare(nodes[i],nodes[i+1],k)==1:
                if i+j>=len(nodes):
                    continue
                if nodes[i+j] not in adj[nodes[i]]:
                    adj[nodes[i]].append(nodes[i+j])
                if nodes[i] not in back_adj[nodes[i+j]]:
                    back_adj[nodes[i+j]].append(nodes[i])


    if is_valid2 and len(adj[nodes[0]]):
        is_valid=True
        for i in adj:
            if len(adj[i])!=len(back_adj[i]):
                is_valid=False
        
        if is_valid:
            print(k)
            break
