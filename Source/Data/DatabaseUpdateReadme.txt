Database Updates
---------------------------------------------------------------------------------------------

WARNING: Schema modifications made at the time-series host application level, i.e., to the
database schema scripts in PrjojectAlpha, will be overwritten when updating the
Grid Solutions Framework dependencies.

Custom schema modifications should go in an independent local script to be run after the
initial schema setup.

Please consult with GPA to make global script modifications in GSF as this will affect all
Time-Series Library hosted implementations. Any changes to the primary schema made in GSF
will get rolled-down to all time-series implementations.

---------------------------------------------------------------------------------------------