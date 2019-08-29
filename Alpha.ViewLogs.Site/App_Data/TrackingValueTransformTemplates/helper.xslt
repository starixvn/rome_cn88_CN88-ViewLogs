<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:variable name="isShowEmptyRow" select="xslLogFormat:ShowEmptyCell()" />

  <xsl:template name="renderRow">
    <xsl:param name="firstKey"/>
    <xsl:param name="firstValue"/>

    <xsl:param name="secondKey" select="''"/>
    <xsl:param name="secondValue"/>

    <xsl:param name="thirdKey" select="''"/>
    <xsl:param name="thirdValue"/>

    <tr>
      <xsl:call-template name="pairKeyValueCells">
        <xsl:with-param name="key" select="$firstKey" />
        <xsl:with-param name="value" select="$firstValue" />
      </xsl:call-template>

      <xsl:call-template name="pairKeyValueCells">
        <xsl:with-param name="key" select="$secondKey" />
        <xsl:with-param name="value" select="$secondValue" />
      </xsl:call-template>

      <xsl:call-template name="pairKeyValueCells">
        <xsl:with-param name="key" select="$thirdKey" />
        <xsl:with-param name="value" select="$thirdValue" />
      </xsl:call-template>
    </tr>
  </xsl:template>

  <xsl:template name="pairKeyValueCells">
    <xsl:param name="key"/>
    <xsl:param name="value"/>
    
    <xsl:if test="$key != ''">
      <td class="colW1">
        <xsl:value-of select="xslLogFormat:GetLogResource($key)" />
      </td>
      <td class="colW2">
        <xsl:value-of select="$value" />
      </td>
    </xsl:if>
  </xsl:template>
  
</xsl:stylesheet>
