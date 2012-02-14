#-*-python-*-
from BaseAI import BaseAI
from GameObject import *

import random

class AI(BaseAI):
  """The class implementing gameplay logic."""
  @staticmethod
  def username():
    return "Shell AI"

  @staticmethod
  def password():
    return "password"

  def init(self):
    pass

  def end(self):
    pass

  def run(self):
    # Print out the current board state
    state = "+---+---+---+---+---+---+---+---+\n"
    for rank in range(8, 0, -1):
      state += "|"
      for file in range(1, 9):
        found = False
        for piece in self.pieces:
          # determines if that piece is at the current rank and file
          if piece.getRank() == rank and piece.getFile() == file:
            found = True
            # Checks if the piece is black
            if piece.getOwner() == 1:
              state += '*'
            else:
              state += ' '
            # prints the piece's type
            state += chr(piece.getType())+" "
        if not found:
          state += "   "
        state += "|"
      state += "\n+---+---+---+---+---+---+---+---+\n"
    print state
    # Looks through information about the players
    for player in self.players:
      print player.getPlayerName(),
      # if playerID is 0, you're white, if its 1, you're black
      if player.getId() == self.playerID():
        print " (ME)",
      print " time remaining:", player.getTime()

    # if there has been a move, print the most recent move
    if len(self.moves):
      print "Last Move Was:\n", self.moves[0]
    # select a random piece and move it to a random position on the board.  Attempts to promote to queen if a promotion happens
    random.choice(self.pieces).move(random.randint(1, 8), random.randint(1, 8), ord('Q'))
    return 1

  def __init__(self, conn):
      BaseAI.__init__(self, conn)
