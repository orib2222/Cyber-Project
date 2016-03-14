#region ----------   ABOUT   -----------------------------
"""
##################################################################
# Created By: ORI BEN ZVI                                        #
# Date: 23/09/2014                                               #
# Name: Encryption & Decryption Script                           #
# Version: 1.0                                                   #
# Windows Tested Versions: Win 7 64-bit                          #
# Python Tested Versions: 2.7 32-bit                             #
# Python Environment  : PyCharm                                  #
# pyCrypto Tested Versions: Python 2.7 32-bit                    #
##################################################################
"""
#endregion

# region--------------------------------------------IMPORTS-----------------------------------------
import pickle
import time

from Crypto import Random
from Crypto.Hash import SHA256
from Crypto.PublicKey import RSA
import base64

from AES import *

# endregion

# region-------------------------------------------CONSTANTS----------------------------------------
KEY_LENGTH = 1024
PORT = 6070
LEN_UNIT_BUF = 2048                                       # Min len of buffer for receive from server socket
MAX_ENCRYPTED_MSG_SIZE = 128
MAX_SOURCE_MSG_SIZE = 128
END_LINE = "\r\n"
# endregion


class Security:
    private_key = None

    # ----------------------------------------------------------
    def __init__(self):
        self.private_key = RSA.generate(KEY_LENGTH, Random.new().read)
        self.key = Random.new().read(int(16))
        self.aes=AESCrypt()

    # region-----------------FUNCTIONS--------------------------
    # ----------------------------------------------------------

    def encrypt_sym_key(self, data, key):
        return key.encrypt(data)
    # ----------------------------------------------------------

    def decrypt_sym_key(self, encrypted, key):
        return key.decrypt(encrypted)
    # ----------------------------------------------------------

    def encrypt(self, data, public_key):
        pack_data = self.pack(data)
        if not public_key:
            public_key = self.private_key.publickey()
        return public_key.encrypt(pack_data, 32)[0]

    # ----------------------------------------------------------
    def decode(self, data, private_key):
        if not private_key:
            private_key = self.private_key
        decrypt_data = private_key.decrypt(data)

        return self.unpack(decrypt_data)

    # ----------------------------------------------------------
    def unpack(self, data):
        return pickle.loads(data)

    # ----------------------------------------------------------
    def pack(self, data):
        return pickle.dumps(data)

    #-----------------------------------------------------------------------------------------------
    #  Key Exchange
    #
    # Description: 
    #-----------------------------------------------------------------------------------------------

    def key_exchange_client(self, socket):
        #--------------------  1 ------------------------------------------------------------------------
        # --------------  Wait server Public_Key --------------------------------------------------------
        # get Pickled public key
        pickled_server_public_key = socket.recv(LEN_UNIT_BUF).split(END_LINE)[0]
        server_public_key = pickle.loads(pickled_server_public_key)
        # --------------  Wait server hash Public_Key -------------------------------------------------------
        # Hashing original Public_Key
        calculated_hash_server_pickled_public_key = SHA256.new(pickle.dumps(server_public_key)).hexdigest()
        declared_hash_server_pickled_public_key = b64decode(socket.recv(LEN_UNIT_BUF).split(END_LINE)[0] )
        if calculated_hash_server_pickled_public_key != declared_hash_server_pickled_public_key:
            return "Not Magic"

        #--------------------  2 ------------------------------------------------------------------------
        # ------------  Send  client private key
        socket.send(pickle.dumps(self.private_key.exportKey()) + END_LINE)
        time.sleep(0.5)
        # -----------  send  Base64 Hash of self.crypto.private_key
        socket.send(b64encode(SHA256.new(pickle.dumps(self.private_key.exportKey())).hexdigest()) + END_LINE)
        time.sleep(0.5)

        #--------------------  3 ------------------------------------------------------------------------
        # -------------- Send  encrypted by server public key info containing symmetric key and hash symmetric key encrypted by client public key ---------------------
        if self.private_key.can_encrypt():
            hash_sym_key = SHA256.new(self.key).hexdigest()
            print str(hash_sym_key)
            pickle_encrypt_hash_sym_key = pickle.dumps(self.private_key.publickey().encrypt(hash_sym_key, 32))
            message = b64encode(self.key) + "#" + b64encode( pickle_encrypt_hash_sym_key )
            print message
            splitted_pickled_message = [message[i:i+MAX_ENCRYPTED_MSG_SIZE] for i in xrange(0, len(message), MAX_ENCRYPTED_MSG_SIZE)]
            #   Sending to server number of encrypted message parts
            socket.send(str(len(splitted_pickled_message)) + END_LINE)
            pickled_encrypted_message = ''
            for part in splitted_pickled_message:
                    part_encrypted_pickled_message = server_public_key.encrypt(part, 32)
                    pickled_part_encrypted_pickled_message = pickle.dumps(part_encrypted_pickled_message)
                    socket.send(pickled_part_encrypted_pickled_message + END_LINE)
                    pickled_encrypted_message += pickled_part_encrypted_pickled_message
                    time.sleep(0.5)
            return self.key


        # endregion
