# Marbles.Fixed.Proportions.Generator

A quick exercise in randomly generating a collection based on a fixed distribution.


===

There are two ways to do this.  The first is to plot the combined probabilities on a line from 0 to 1.  We do this by "correcting" the weights of the distribution (i.e., combined probability of the marbles must add up to 1).  We then loop from zero to `count`, generating a random float from 0 to 1 each time.  If/Else blocks will take care of picking the marbles along the number line.

The second method is to divide marble distributions by the greatest common factor.  The reason for this is that (20, 10, 5, 5) behaves in the exact same way as (4, 2, 1, 1).  We then put the marbles by their count into a `masterBucket`.  We loop the same, picking random marbles from the bucket each time.

Both methods assumes that the random number generator is uniformly distributed (no number is favoured over the other).
