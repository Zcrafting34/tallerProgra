# Lógica de IA organizada por Estados
class EstadoEnemigo:
    PATRULLA = "PATRULLA"
    ALERTA = "ALERTA"
    PERSECUCION = "PERSECUCION"
    ATAQUE = "ATAQUE"

class ChasqueadorFSM:
    def __init__(self):
        self.estado_actual = EstadoEnemigo.PATRULLA

    def procesar_ia(self, nivel_ruido, distancia_jugador):
        if self.estado_actual == EstadoEnemigo.PATRULLA:
            if nivel_ruido > 10:
                self.cambiar_estado(EstadoEnemigo.ALERTA)

        elif self.estado_actual == EstadoEnemigo.ALERTA:
            if nivel_ruido > 50:
                self.cambiar_estado(EstadoEnemigo.PERSECUCION)
            elif nivel_ruido == 0:
                self.cambiar_estado(EstadoEnemigo.PATRULLA)

        elif self.estado_actual == EstadoEnemigo.PERSECUCION:
            if distancia_jugador < 1.5:
                self.cambiar_estado(EstadoEnemigo.ATAQUE)

        self.ejecutar_accion()

    def ejecutar_accion(self):
        acciones = {
            "PATRULLA": "Caminando en círculos y emitiendo chasquidos leves.",
            "ALERTA": "Se detiene, inclina la cabeza y emite chasquidos fuertes.",
            "PERSECUCION": "Corre hacia la fuente del ruido gritando.",
            "ATAQUE": "Animación de agarre y muerte del jugador."
        }
        print(f"[ESTADO: {self.estado_actual}] -> {acciones[self.estado_actual]}")

    def cambiar_estado(self, nuevo_estado):
        print(f"--- Transición: {self.estado_actual} -> {nuevo_estado} ---")
        self.estado_actual = nuevo_estado