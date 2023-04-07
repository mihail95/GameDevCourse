from itertools import product
from random import randint

# If charge attack dosent end turn, and block only blocks once
# so, charge attack costs computer one turn, defend costs player one turn and stops one attack
# - then chage attack is just and empty turn and result is still fully determined by attacks and heals

turns_to_kill = 5
times_healed = 3
heal_amount = 3
vals = [2, 3, 4]

cart_product = set(product(vals, repeat=turns_to_kill))
cart_product_with_skip = []

for comb in cart_product:
    new_comb = []
    for val in comb:
        rand_val = randint(1, 100)
        if rand_val <= 35:
            new_comb.append(0)
        else:
            new_comb.append(val)

    cart_product_with_skip.append(tuple(new_comb))
            


# print (sorted([x for x in cart_product if x[0] == 2]))
# print (sorted([x for x in cart_product if x[0] == 3]))
# print (sorted([x for x in cart_product if x[0] == 4]))

# print ('-----------------------OLD-----------------------')
# print (cart_product)
# print ('-----------------------NEW-----------------------')
# print (cart_product_with_skip)

kill_threshold = 10 + heal_amount*times_healed

all_kills_old = [val for val in cart_product if sum(val) >= kill_threshold]
all_kills_new = [val for val in cart_product_with_skip if sum(val) >= kill_threshold]

# print ("All kills charge takes one turn:")
# print (all_kills_old)
# print ("All kills charge takes two turns:")
# print (all_kills_new)

# print ('')

print ("All kills charge takes one turn:")
print (len(all_kills_old) / len(cart_product) * 100)
print ("All kills charge takes two turns:")
print (len(all_kills_new) / len(cart_product) * 100)


# Player
turns_to_kill_player = (turns_to_kill + 1) - times_healed
vals = [1, 2]

cart_product = set(product(vals, repeat=turns_to_kill_player))
kill_threshold = 10

all_kills_old = [val for val in cart_product if sum(val) >= kill_threshold]

print ("PLAYER - All kills charge takes one turn:")
print (len(all_kills_old) / len(cart_product) * 100)